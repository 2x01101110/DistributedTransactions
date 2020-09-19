using System;

namespace TravelAgency.Components.CourierActivities.BookHotelActivity
{
    public interface IBookHotelActivityArguments
    {
        Guid HotelId { get; }
        Guid RoomId { get; }
        DateTime CheckIn { get; }
        DateTime CheckOut { get; }
    }
}
