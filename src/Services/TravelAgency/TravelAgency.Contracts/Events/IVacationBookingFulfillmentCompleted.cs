using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Events
{
    public interface IVacationBookingFulfillmentCompleted
    {
        Guid VacationId { get; }
        DateTime Timestamp { get; }
    }
}
