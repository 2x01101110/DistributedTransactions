using System;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Contracts.Commands.FulfillVacationBooking
{
    public interface IFulfillVacationBooking
    {
        Guid VacationId { get; }
        IHotelBookingInformation HotelBookingInformation { get; }
        IFlightBookingInformation FlightBookingInformation { get; }
        IVacationExtras VacationExtras { get; }
    }
}
