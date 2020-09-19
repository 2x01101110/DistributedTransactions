using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Contracts.Commands;

namespace TravelAgency.Components.CourierActivities.BookHotelActivity
{
    public interface IBookHotelActivityArguments
    {
        Guid CustomerId { get; }
        DateTime BookFrom { get; }
        DateTime BookTo { get; }
    }
}
