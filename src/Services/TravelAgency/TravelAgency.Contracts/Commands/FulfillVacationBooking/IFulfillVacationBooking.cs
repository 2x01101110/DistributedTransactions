using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands.FulfillVacationBooking
{
    public interface IFulfillVacationBooking
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
    }
}
