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

            builder.AddActivity("BookHotel", new Uri("queue:book-hotel_execute"), new 
            { 
                context.Message.HotelBookingInformation.CheckIn,
                context.Message.HotelBookingInformation.CheckOut,
                context.Message.HotelBookingInformation.HotelId,
                context.Message.HotelBookingInformation.RoomId,
            });

            builder.AddActivity("BookFlight", new Uri("queue:book-flight_execute"), new 
            {
                context.Message.FlightBookingInformation.AirportId,
                context.Message.FlightBookingInformation.DepartureDate,
                context.Message.FlightBookingInformation.DestinationId,
                context.Message.FlightBookingInformation.ReturnDate,
                context.Message.FlightBookingInformation.ReturnId
            });

            if (context.Message.VacationExtras.CarRental != null)
            {
                builder.AddActivity("RentCar", new Uri("queue:rent-car_execute"), new 
                {
                    context.Message.VacationExtras.CarRental.CarId,
                    context.Message.VacationExtras.CarRental.RentFrom,
                    context.Message.VacationExtras.CarRental.RentTo
                });
            }

            var routingSlip = builder.Build();

            await context.Execute(routingSlip);
        }
    }
}
