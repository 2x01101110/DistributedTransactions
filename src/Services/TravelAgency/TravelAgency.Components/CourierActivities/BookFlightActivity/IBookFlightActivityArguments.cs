using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Components.CourierActivities.BookFlightActivity
{
    public interface IBookFlightActivityArguments
    {
        Guid DepartureFlightId { get; }
        Guid ReturnFlightId { get; }
    }
}
