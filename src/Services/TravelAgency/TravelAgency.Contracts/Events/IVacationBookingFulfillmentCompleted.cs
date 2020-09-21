using System;

namespace TravelAgency.Contracts.Events
{
    public interface IVacationBookingFulfillmentCompleted
    {
        Guid VacationId { get; }
        DateTime Timestamp { get; }
    }
}
