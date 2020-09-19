using System;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Contracts.Masstransit.Events;

namespace TravelAgency.API.Models
{
    public class VacationBookingModel
    {
        [Required]
        public Guid DealId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        public VacationExtras VacationExtras { get; set; }
    }
}
