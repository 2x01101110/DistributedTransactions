using System;

namespace TravelAgency.Components.CourierActivities.BookFlightActivity
{
    public interface IBookFlightActivityLog
    {
        Guid DepartureFlightId { get; }
        Guid ReturnFLightId { get; }
    }
}
