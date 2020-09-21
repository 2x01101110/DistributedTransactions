using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Events
{
    public interface IVacationBookingFulfillmentFailed
    {
        Guid VacationId { get; }
        string Reason { get; }
    }
}
