using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Events.HotelReservation
{
    public interface IReserveHotel
    {
        Guid HotelId { get; }
        Guid RoomId { get; }
        DateTime CheckInDate { get; }
        DateTime CheckOutDate { get; }
    }
}
