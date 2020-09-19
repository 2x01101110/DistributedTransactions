using System;

namespace Shared.Contracts.Events.HotelBooking
{
    public interface IBookHotel
    {
        Guid HotelId { get; }
        Guid RoomId { get; }
        DateTime CheckInDate { get; }
        DateTime CheckOutDate { get; }
    }
}
