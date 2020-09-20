using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Contracts.Commands.FulfillVacationBooking;
using TravelAgency.Contracts.Events;
using TravelAgency.Models;

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

            builder.AddActivity("BookHotel", new Uri("queue:book-hotel_execute"), context.Message.FlightBookingInformation);

            builder.AddActivity("BookFlight", new Uri("queue:book-flight_execute"), context.Message.HotelBookingInformation);

            if (context.Message.VacationExtras.CarRental != null)
            {
                builder.AddActivity("RentCar", new Uri("queue:rent-car_execute"), context.Message.VacationExtras.CarRental);
            }

            await builder.AddSubscription(context.SourceAddress,
                RoutingSlipEvents.Completed | RoutingSlipEvents.Supplemental,
                RoutingSlipEventContents.None, x => x.Send<IVacationBookingFulfillmentCompleted>(new
                {
                    context.Message.VacationId,
                    Timestamp = DateTime.UtcNow
                }));

            var routingSlip = builder.Build();

            await context.Execute(routingSlip);
        }
    }
}
