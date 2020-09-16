using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest
{
    public interface IVacationBookingProcessStateRequest
    {
        Guid DealId { get; }
    }
}
