using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest
{
    public interface IVacationBookingProcessState
    {
        Guid DealId { get; }
        string State { get; }
    }
}
