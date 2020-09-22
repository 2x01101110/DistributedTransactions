using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.Events.HotelBooking;
using Shared.Contracts.HotelBooking;
using System;
using System.Threading.Tasks;

namespace HotelBooking.Components.Consumers
{
    public class ReserveHotelConsumer : 
        IConsumer<IBookHotel>,
        IConsumer<ICancelHotelBooking>
    {
        private readonly ILogger<ReserveHotelConsumer> _logger;

        public ReserveHotelConsumer(ILogger<ReserveHotelConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IBookHotel> context)
        {
            this._logger.LogInformation($"Processing {nameof(IBookHotel)} event" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

            await context.RespondAsync<IHotelBookingAccepted>(new
            {
                BookingId = Guid.NewGuid()
            });
        }

        public Task Consume(ConsumeContext<ICancelHotelBooking> context)
        {
            this._logger.LogWarning($"Processing {nameof(ICancelHotelBooking)} event" +
                $"Canceling hotel booking: {context.Message.BookingId}");

            return Task.CompletedTask;
        }
    }
}
