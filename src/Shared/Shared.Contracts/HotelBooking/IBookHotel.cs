using System;

namespace Shared.Contracts.Events.HotelBooking
{
    public interface IBookHotel
    {
        Guid HotelId { get; }
        Guid RoomId { get; }
        DateTime CheckIn { get; }
        DateTime CheckOut { get; }
    }
}
