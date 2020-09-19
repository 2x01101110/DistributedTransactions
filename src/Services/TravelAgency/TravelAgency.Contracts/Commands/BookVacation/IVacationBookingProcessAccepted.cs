using System;

namespace TravelAgency.Contracts.Commands.BookVacation
{
    public interface IVacationBookingProcessAccepted
    {
        Guid VacationId { get; }
    }
}
