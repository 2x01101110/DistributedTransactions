using System;

namespace TravelAgency.Contracts.Commands.BookVacation
{
    public interface IVacationBookingProcessAccepted
    {
        Guid VacationId { get; }
        Guid DealId { get; }
        Guid CustomerId { get; }
        IHotel Hotel { get; }
        int TravelClass { get; }
        Guid? CarId { get; }
    }
}
