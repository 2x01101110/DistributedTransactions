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
    }
}
