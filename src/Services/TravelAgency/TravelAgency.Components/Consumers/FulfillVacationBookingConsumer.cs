using MassTransit;
using MassTransit.Courier;
using System;
using System.Threading.Tasks;
using TravelAgency.Contracts.Commands.FulfillVacationBooking;

namespace TravelAgency.Components.Consumers
{
    public class FulfillVacationBookingConsumer : IConsumer<IFulfillVacationBooking>
    {
        public async Task Consume(ConsumeContext<IFulfillVacationBooking> context)
        {
            var builder = new RoutingSlipBuilder(NewId.NextGuid());

            builder.AddActivity("RentCar", new Uri("queue:rent-car_execute"), new { });

            var routingSlip = builder.Build();

            await context.Execute(routingSlip);
        }
    }
}
