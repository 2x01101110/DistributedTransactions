using System;
using System.Collections.Generic;
using TravelAgency.Contracts.Commands;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public interface IVacationBookingProcessStarted
    {
        Guid VacationId { get; }
        Guid CustomerId { get; }

        HotelBookingInformation HotelBookingInformation { get; }
        FlightBookingInformation FlightBookingInformation { get; }

        VacationExtras VacationExtras { get; }
    }
}
