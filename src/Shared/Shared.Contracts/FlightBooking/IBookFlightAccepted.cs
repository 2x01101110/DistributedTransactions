using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.FlightBooking
{
    public interface IBookFlightAccepted
    {
        Guid DepartureFlightId { get; }
        Guid ReturnFLightId { get; }
    }
}
