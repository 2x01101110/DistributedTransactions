using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands.BookVacation
{
    public interface IBookVacationProcessAccepted
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
    }
}
