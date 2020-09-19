using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Events.HotelBooking
{
    public interface IHotelBookingAccepted
    {
        Guid BookingId { get; }
    }
}
