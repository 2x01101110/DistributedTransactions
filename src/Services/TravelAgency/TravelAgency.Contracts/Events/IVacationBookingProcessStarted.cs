using System;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public interface IVacationBookingProcessStarted
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
    }
}
