﻿using System;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public class HotelBookingInformation
    {
        public Guid HotelId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
