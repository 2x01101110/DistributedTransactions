using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Events
{
    public interface ICarRented
    {
        Guid CarId { get; }
        DateTime RentFrom { get; }
        DateTime RentTo { get; }
    }
}
