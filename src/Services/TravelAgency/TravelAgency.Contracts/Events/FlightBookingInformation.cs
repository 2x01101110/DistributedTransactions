using System;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public class FlightBookingInformation
    {
        public Guid AirportId { get; set; }
        public DateTime DepartureDate { get; set; }
        public Guid DestinationId { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid ReturnId { get; set; }
    }
}
