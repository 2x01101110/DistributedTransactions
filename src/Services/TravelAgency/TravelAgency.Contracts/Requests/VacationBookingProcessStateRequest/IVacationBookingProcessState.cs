using System;

namespace TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest
{
    public interface IVacationBookingProcessState
    {
        Guid VacationId { get; }
        string State { get; }
    }
}
