using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.CarRental
{
    public interface ICancelCarRental
    {
        Guid CarRentalId { get; }
    }
}
