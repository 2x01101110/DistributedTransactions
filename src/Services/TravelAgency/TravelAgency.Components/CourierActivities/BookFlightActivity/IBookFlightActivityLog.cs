using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Components.CourierActivities.BookFlightActivity
{
    public interface IBookFlightActivityLog
    {
        Guid DepartureFlightId { get; }
        Guid ReturnFlightId { get; }
    }
}
