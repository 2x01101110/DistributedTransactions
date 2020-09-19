using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.FlightBooking
{
    public interface IBookFlightRejected
    {
        string Reason { get; }
    }
}
