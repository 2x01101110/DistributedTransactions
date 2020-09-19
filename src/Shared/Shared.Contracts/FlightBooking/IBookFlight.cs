using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.FlightBooking
{
    public interface IBookFlight
    {
        Guid AirportId { get; }
        DateTime DepartureDate { get; }
        Guid DestinationId { get; }
        DateTime ReturnDate { get; }
        Guid ReturnId { get; }
    }
}
