using MassTransit;
using Shared.Contracts.FlightBooking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Components.Consumers
{
    public class FlightBookingConsumer : IConsumer<IBookFlight>
    {
        public async Task Consume(ConsumeContext<IBookFlight> context)
        {
            await context.RespondAsync<IBookFlightAccepted>(new
            {
                DepartureFlightId = Guid.NewGuid(),
                ReturnFLightId = Guid.NewGuid()
            });
        }
    }
}
