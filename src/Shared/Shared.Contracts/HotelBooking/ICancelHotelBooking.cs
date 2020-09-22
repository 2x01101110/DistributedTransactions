using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.HotelBooking
{
    public interface ICancelHotelBooking
    {
        Guid BookingId { get; }
    }
}
