using System;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Models
{
    public class HotelBookingInformation : IHotelBookingInformation
    {
        public Guid HotelId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
