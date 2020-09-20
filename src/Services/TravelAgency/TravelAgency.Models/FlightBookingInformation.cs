using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Models
{
    public class FlightBookingInformation : IFlightBookingInformation
    {
        public Guid AirportId { get; set; }
        public DateTime DepartureDate { get; set; }
        public Guid DestinationId { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid ReturnId { get; set; }
    }
}
