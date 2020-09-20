using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.CarRenting
{
    public interface ICarRentalRejected
    {
        string Reason { get; }
    }
}
