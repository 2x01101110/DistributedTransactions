using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Components.CourierActivities.BookHotelActivity
{
    public interface IBookHotelActivityLog
    {
        Guid BookingId { get; }
    }
}
