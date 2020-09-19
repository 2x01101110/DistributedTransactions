using Automatonymous;
using MassTransit.Saga;
using System;
using TravelAgency.Contracts.Commands;

namespace TravelAgency.Components.StateMachines.VacationBooking
{
    public class VacationBookingProcessState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
        public string State { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        
        public Guid VacationId { get; set; }
        public Guid CustomerId { get; set; }


        /*public Guid HotelId { get; }
        public Guid RoomId { get; }

        public DateTime VacationStart { get; set; }
        public DateTime VacationEnd { get; set; }
        
        public Guid? CarId { get; set; }*/

        /*Guid VacationId { get; }
        Guid CustomerId { get; }

        Guid HotelId { get; }
        Guid RoomId { get; }

        DateTime Departure { get; }
        DateTime Return { get; }

        IVacationExtras VacationExtras { get; }*/
    }
}
