using MassTransit;
using Shared.Contracts.Events.HotelBooking;
using System;
using System.Threading.Tasks;

namespace HotelReservation.Components.Consumers
{
    public class ReserveHotelConsumer : IConsumer<IBookHotel>
    {
        public Task Consume(ConsumeContext<IBookHotel> context)
        {
            throw new NotImplementedException();
        }
    }
}
