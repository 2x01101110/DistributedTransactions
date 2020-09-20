using System;

namespace TravelAgency.Contracts.Commands.SharedCommandContracts
{
    public interface IFlightBookingInformation
    {
        Guid AirportId { get; }
        DateTime DepartureDate { get; }
        Guid DestinationId { get; }
        DateTime ReturnDate { get; }
        Guid ReturnId { get; }
    }
}
