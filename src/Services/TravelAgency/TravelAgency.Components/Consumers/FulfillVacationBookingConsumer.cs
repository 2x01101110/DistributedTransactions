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

            if (context.Message.CarId != null)
            {
                builder.AddActivity("RentCar", new Uri("queue:rent-car_execute"), new
                {
                    context.Message.CarId,
                    RentFrom = context.Message.VacationStart,
                    RentTo = context.Message.VacationEnd
                });
            }

            var routingSlip = builder.Build();

            await context.Execute(routingSlip);
        }
    }
}
