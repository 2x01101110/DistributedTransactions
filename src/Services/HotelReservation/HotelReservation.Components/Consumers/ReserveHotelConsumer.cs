using MassTransit;
using Shared.Contracts.Events.HotelBooking;
using System;
using System.Threading.Tasks;

namespace HotelReservation.Components.Consumers
{
    public class ReserveHotelConsumer : IConsumer<IBookHotel>
    {
        public async Task Consume(ConsumeContext<IBookHotel> context)
        {
            await context.RespondAsync<IHotelBookingAccepted>(new
            {
                BookingId = Guid.NewGuid()
            });
        }
    }
}
