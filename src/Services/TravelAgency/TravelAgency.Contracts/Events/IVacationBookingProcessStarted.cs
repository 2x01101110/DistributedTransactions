using System;
using TravelAgency.Contracts.Commands;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public interface IVacationBookingProcessStarted
    {
        Guid VacationId { get; }
        DateTime VacationStart { get; }
        DateTime VacationEnd { get; }
        Guid DealId { get; }
        Guid CustomerId { get; }
        IHotel Hotel { get; }
        int TravelClass { get; }
        Guid? CarId { get; }
    }
}
