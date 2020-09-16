using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest
{
    public interface IVacationBookingProcessStateNotFound
    {
        Guid DealId { get; }
    }
}
