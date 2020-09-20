using System;

namespace TravelAgency.Contracts.Commands.SharedCommandContracts
{
    public interface ICarRental
    {
        Guid CarId { get; }
        DateTime RentFrom { get; }
        DateTime RentTo { get; }
    }
}
