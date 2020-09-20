using System;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Contracts.Commands
{
    public interface IBookVacation
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
        IVacationExtras VacationExtras { get; }
    }
}
