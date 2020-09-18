using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands.FulfillVacationBooking
{
    public interface IFulfillVacationBooking
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
