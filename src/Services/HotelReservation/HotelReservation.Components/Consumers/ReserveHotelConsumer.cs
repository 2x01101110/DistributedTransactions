using MassTransit;
using Shared.Contracts.Events.HotelReservation;
using System;
using System.Threading.Tasks;

namespace HotelReservation.Components.Consumers
{
    public class ReserveHotelConsumer : IConsumer<IReserveHotel>
    {
        public Task Consume(ConsumeContext<IReserveHotel> context)
        {
            throw new NotImplementedException();
        }
    }
}
