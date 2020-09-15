using System;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public interface IStartVacationBookingProcess
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
    }
}
