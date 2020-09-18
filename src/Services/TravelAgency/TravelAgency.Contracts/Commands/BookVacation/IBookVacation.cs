using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands
{
    public interface IBookVacation
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
        IHotel Hotel { get; }
        int TravelClass { get; }
        Guid? CarId { get; }
    }

    public class Hotel : IHotel
    {
        public Guid HotelId { get; set; }
        public Guid RoomId { get; set; }
    }

    public interface IHotel
    {
        Guid HotelId { get; }
        Guid RoomId { get; }
    }
}
