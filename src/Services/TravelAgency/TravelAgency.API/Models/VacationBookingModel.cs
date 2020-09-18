using System;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Contracts.Commands;

namespace TravelAgency.API.Models
{
    public class VacationBookingModel
    {
        [Required]
        public Guid DealId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Hotel Hotel { get; set; }

        [Required]
        [Range(0, 2)]
        // 0 = First class 1 = Business class 2 = Eveonomy class
        public int TravelClass { get; set; }

        // Optional, customer may rent a car for vacation through the travel agency service
        public Guid? CarId { get; set; }
    }
}
