using System;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Contracts.Commands.SharedCommandContracts;
using TravelAgency.Models;

namespace TravelAgency.API.Models
{
    public class VacationBookingModel
    {
        [Required]
        public Guid DealId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public VacationExtras VacationExtras { get; set; }
    }
}
