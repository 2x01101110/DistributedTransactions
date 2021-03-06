﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Components.CourierActivities.RentCarActivity
{
    public interface IRentCarActivityArguments
    {
        Guid CarId { get; }
        DateTime RentFrom { get; }
        DateTime RentTo { get; }
    }
}
