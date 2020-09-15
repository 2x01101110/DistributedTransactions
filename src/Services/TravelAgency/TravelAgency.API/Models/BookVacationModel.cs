using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.API.Models
{
    public class BookVacationModel
    {
        [Required]
        public Guid DealId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}
