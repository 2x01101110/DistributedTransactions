using System;
using System.Collections.Generic;
using TravelAgency.Contracts.Commands;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public interface IVacationBookingProcessStarted
    {
        Guid VacationId { get; }
        Guid CustomerId { get; }

        Guid HotelId { get; }
        Guid RoomId { get; }

        DateTime Departure { get; }
        DateTime Return { get; }

        VacationExtras VacationExtras { get; }
    }
}
