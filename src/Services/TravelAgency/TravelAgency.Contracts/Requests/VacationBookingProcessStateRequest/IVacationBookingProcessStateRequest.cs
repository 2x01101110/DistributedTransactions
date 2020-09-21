using System;

namespace TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest
{
    public interface IVacationBookingProcessStateRequest
    {
        Guid VacationId { get; }
    }
}
