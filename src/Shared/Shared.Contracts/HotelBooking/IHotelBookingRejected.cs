using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Events.HotelBooking
{
    public interface IHotelBookingRejected
    {
        string Reason { get; }
    }
}
