using System;
using TravelAgency.Contracts.Masstransit.Events;

namespace TravelAgency.Contracts.Commands
{
    public interface IBookVacation
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
        VacationExtras VacationExtras { get; }
    }
}
