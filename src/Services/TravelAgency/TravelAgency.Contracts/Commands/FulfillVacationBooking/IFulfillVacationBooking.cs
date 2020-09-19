using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Contracts.Masstransit.Events;

namespace TravelAgency.Contracts.Commands.FulfillVacationBooking
{
    public interface IFulfillVacationBooking
    {
        Guid VacationId { get; }
        HotelBookingInformation HotelBookingInformation { get; }
        FlightBookingInformation FlightBookingInformation { get; }
        VacationExtras VacationExtras { get; }
    }
}
