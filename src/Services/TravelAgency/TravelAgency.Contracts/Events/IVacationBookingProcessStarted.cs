﻿using System;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public interface IVacationBookingProcessStarted
    {
        Guid VacationId { get; }
        Guid CustomerId { get; }
        IHotelBookingInformation HotelBookingInformation { get; }
        IFlightBookingInformation FlightBookingInformation { get; }
        IVacationExtras VacationExtras { get; }
    }
}
