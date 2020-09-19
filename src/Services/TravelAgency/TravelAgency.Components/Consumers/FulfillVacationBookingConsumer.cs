using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TravelAgency.Contracts.Commands.FulfillVacationBooking;

namespace TravelAgency.Components.Consumers
{
    public class FulfillVacationBookingConsumer : IConsumer<IFulfillVacationBooking>
    {
        private readonly ILogger<FulfillVacationBookingConsumer> _logger;

        public FulfillVacationBookingConsumer(ILogger<FulfillVacationBookingConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IFulfillVacationBooking> context)
        {
            this._logger.LogInformation($"{nameof(FulfillVacationBookingConsumer)} building & executing Routing Slip" +
                $"\r\nPayload: {context.Message}");

            var builder = new RoutingSlipBuilder(NewId.NextGuid());

            if (context.Message.VacationExtras.CarId != null)
            {
                builder.AddActivity("RentCar", new Uri("queue:rent-car_execute"), new
                {
                    context.Message.VacationExtras.CarId,
                    RentFrom = context.Message.Departure,
                    RentTo = context.Message.Return
                });
            }

            var routingSlip = builder.Build();

            await context.Execute(routingSlip);
        }
    }
}
