using Automatonymous;
using MassTransit.Saga;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Components.StateMachines.VacationBooking
{
    public class VacationBookingProcessState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
        public string State { get; set; }
        public DateTime? Updated { get; set; }

        public Guid DealId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
