using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Contracts.Commands.SharedCommandContracts
{
    public interface IVacationExtras
    {
        ICarRental CarRental { get; }
    }
}
