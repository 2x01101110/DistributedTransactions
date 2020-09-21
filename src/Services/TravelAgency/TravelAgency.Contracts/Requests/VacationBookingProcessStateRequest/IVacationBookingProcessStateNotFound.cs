using System;

namespace TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest
{
    public interface IVacationBookingProcessStateNotFound
    {
        Guid VacationId { get; }
    }
}
