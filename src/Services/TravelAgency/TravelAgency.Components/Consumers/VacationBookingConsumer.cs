using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.BookVacation;
using TravelAgency.Contracts.Masstransit.Events;
using TravelAgency.Models;

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

                var vacationId = Guid.NewGuid();

                await context.Publish<IVacationBookingProcessStarted>(new
                {
                    VacationId = vacationId,
                    context.Message.CustomerId,
                    HotelBookingInformation = new
                    {
                        HotelId = Guid.NewGuid(),
                        RoomId = Guid.NewGuid(),
                        CheckIn = DateTime.UtcNow.AddDays(20).Day,
                        CheckOut = DateTime.UtcNow.AddDays(27).Day
                    },
                    FlightBookingInformation = new
                    {
                        AirportId = Guid.NewGuid(),
                        DepartureDate = DateTime.UtcNow.AddDays(19).Day,
                        DestinationId = Guid.NewGuid(),
                        ReturnDate = DateTime.UtcNow.AddDays(27).Day,
                        ReturnId = Guid.NewGuid()
                    },
                    context.Message.VacationExtras
                });

                await context.RespondAsync<IVacationBookingProcessAccepted>(new
                {
                    VacationId = vacationId
                });
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
