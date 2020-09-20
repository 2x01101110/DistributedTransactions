using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Models
{
    public class VacationExtras : IVacationExtras
    {
        public ICarRental CarRental { get; set; }
    }
}
