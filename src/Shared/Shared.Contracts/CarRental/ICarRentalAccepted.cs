using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.CarRenting
{
    public interface ICarRentalAccepted
    {
        Guid CarRentalId { get; }
    }
}
