using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands.BookVacation
{
    public interface IBookVacationProcessRejected
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
        string Reason { get; }
    }
}
