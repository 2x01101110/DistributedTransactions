using System;

namespace TravelAgency.Contracts.Commands.SharedCommandContracts
{
    public interface IVacationExtras
    {
        ICarRental CarRental { get; }
    }
}
