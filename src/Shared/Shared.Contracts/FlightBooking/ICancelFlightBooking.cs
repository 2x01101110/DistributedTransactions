using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.FlightBooking
{
    public interface ICancelFlightBooking
    {
        Guid DepartureFlightId { get; }
        Guid ReturnFLightId { get; }
    }
}
