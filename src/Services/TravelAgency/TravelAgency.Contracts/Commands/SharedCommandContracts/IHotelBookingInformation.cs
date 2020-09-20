using System;

namespace TravelAgency.Contracts.Commands.SharedCommandContracts
{
    public interface IHotelBookingInformation
    {
        Guid HotelId { get;}
        Guid RoomId { get; }
        DateTime CheckIn { get; }
        DateTime CheckOut { get; }
    }
}
