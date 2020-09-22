using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.FlightBooking;
using System;
using System.Threading.Tasks;

namespace FlightBooking.Components.Consumers
{
    public class FlightBookingConsumer : 
        IConsumer<IBookFlight>,
        IConsumer<ICancelFlightBooking>
    {
        private readonly ILogger<FlightBookingConsumer> _logger;

        public FlightBookingConsumer(ILogger<FlightBookingConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IBookFlight> context)
        {
            this._logger.LogInformation($"Processing {nameof(IBookFlight)} event" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

            await context.RespondAsync<IBookFlightAccepted>(new
            {
                DepartureFlightId = Guid.NewGuid(),
                ReturnFLightId = Guid.NewGuid()
            });
        }

        public Task Consume(ConsumeContext<ICancelFlightBooking> context)
        {
            this._logger.LogWarning($"Processing {nameof(ICancelFlightBooking)} event" +
                $"Canceling flight booking: {context.Message.DepartureFlightId} {context.Message.ReturnFLightId}");

            return Task.CompletedTask;
        }
    }
}
