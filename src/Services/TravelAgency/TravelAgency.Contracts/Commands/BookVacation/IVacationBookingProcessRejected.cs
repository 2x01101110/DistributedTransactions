using System;

namespace TravelAgency.Contracts.Commands.BookVacation
{
    public interface IVacationBookingProcessRejected
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
        string Reason { get; }
    }
}
