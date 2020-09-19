using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands
{
    public interface IBookVacation
    {
        Guid DealId { get; }
        Guid CustomerId { get; }
        VacationExtras VacationExtras { get; }
    }

    public class VacationExtras
    { 
        public Guid? CarId { get; set; }
    }
}
