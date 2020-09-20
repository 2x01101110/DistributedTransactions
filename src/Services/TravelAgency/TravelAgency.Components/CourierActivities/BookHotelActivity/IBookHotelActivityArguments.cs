using System;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Components.CourierActivities.BookHotelActivity
{
    public interface IBookHotelActivityArguments : IHotelBookingInformation
    {
    }
}
