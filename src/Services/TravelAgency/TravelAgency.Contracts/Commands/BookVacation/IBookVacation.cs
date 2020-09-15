using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands
{
    public interface IBookVacation
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
    }
}
