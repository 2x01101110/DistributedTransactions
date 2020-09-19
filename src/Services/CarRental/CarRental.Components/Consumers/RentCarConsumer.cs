using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.CarRenting;
using System;
using System.Threading.Tasks;

namespace CarRental.Components.Consumers
{
    public class RentCarConsumer : IConsumer<IRentCar>
    {
        private readonly ILogger<RentCarConsumer> _logger;

        public RentCarConsumer(ILogger<RentCarConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IRentCar> context)
        {
            this._logger.LogInformation($"Processing {nameof(IRentCar)} event" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

            await context.RespondAsync<ICarRentalAccepted>(new
            {
                CarRentalId = Guid.NewGuid()
            });
        }
    }
}
