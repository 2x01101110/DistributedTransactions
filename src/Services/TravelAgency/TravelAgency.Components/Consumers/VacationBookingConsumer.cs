using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.BookVacation;
using TravelAgency.Contracts.Masstransit.Events;

namespace TravelAgency.Components.Consumers
{
    public class VacationBookingConsumer : IConsumer<IBookVacation>
    {
        private readonly ILogger<VacationBookingConsumer> _logger;

        public VacationBookingConsumer(ILogger<VacationBookingConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IBookVacation> context)
        {
            try
            {
                this._logger.LogInformation($"Publishing {nameof(IVacationBookingProcessStarted)} event" +
                    $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

                // Assign unique Id to each vacation/vacation process
                // Multiple customers may reference same DealId and it's theoretically possible
                // for same client to book vacation twice, for example for himself and later for friends
                var vacationId = Guid.NewGuid();

                // We woudl probably get vacation duration - start/end time from db by date, not something user passes
                var from = DateTime.UtcNow.Date.AddDays(-7);
                var to = DateTime.UtcNow.Date;

                var state = new
                {
                    VacationId = vacationId,
                    VacationStart = from,
                    VacationEnd = to,
                    context.Message.DealId,
                    context.Message.CustomerId,
                    Hotel = new
                    {
                        context.Message.Hotel.HotelId,
                        context.Message.Hotel.RoomId
                    },
                    context.Message.TravelClass,
                    context.Message.CarId
                };

                await context.Publish<IVacationBookingProcessStarted>(state);

                await context.RespondAsync<IVacationBookingProcessAccepted>(state);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"An error occured in {nameof(IBookVacation)} consumer" +
                    $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}" +
                    $"\r\nError: {ex.Message} {ex.StackTrace}");

                await context.RespondAsync<IVacationBookingProcessRejected>(new
                {
                    context.Message.DealId,
                    context.Message.CustomerId,
                    Reason = "Internal error occured"
                });
            }
        }
    }
}
