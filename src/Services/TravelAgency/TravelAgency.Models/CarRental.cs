﻿using System;
using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Models
{
    public class CarRental : ICarRental
    {
        public Guid CarId { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
    }
}
