using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Domain.Models.Reservations
{
    public class Reservation
    {
        private Reservation()
        {

        }

        public static Reservation CreateReservation()
        {
            return new Reservation();
        }
    }
}
