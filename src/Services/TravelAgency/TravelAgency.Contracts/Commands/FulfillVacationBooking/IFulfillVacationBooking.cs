using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands.FulfillVacationBooking
{
    public interface IFulfillVacationBooking
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
