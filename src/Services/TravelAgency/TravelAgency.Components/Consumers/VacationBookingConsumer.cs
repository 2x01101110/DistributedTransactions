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

                var publishReturnMessage = new
                {
                    VacationId = Guid.NewGuid(),
                    context.Message.CustomerId,

                    // We would extract Hotel, RoomId, Departure, Return values from DB
                    HotelId = Guid.NewGuid(),
                    RoomId = Guid.NewGuid(),
                    Departure = DateTime.UtcNow.Date.AddDays(20),
                    Return = DateTime.UtcNow.Date.AddDays(34),
                    
                    context.Message.VacationExtras
                };

                await context.Publish<IVacationBookingProcessStarted>(publishReturnMessage);

                await context.RespondAsync<IVacationBookingProcessAccepted>(publishReturnMessage);
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
