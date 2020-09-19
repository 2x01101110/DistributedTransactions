using System;

namespace TravelAgency.Contracts.Masstransit.Events
{
    public class CarRental
    {
        public Guid CarId { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
    }
}
