using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Events
{
    public interface IRentCar
    {
        Guid CarId { get; }
        DateTime RentFrom { get; }
        DateTime RentTo { get; }
    }
}
