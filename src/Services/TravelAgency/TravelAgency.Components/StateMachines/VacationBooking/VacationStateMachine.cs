using Automatonymous;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Contracts.Masstransit.Events;

namespace TravelAgency.Components.StateMachines.VacationBooking
{
    public class VacationStateMachine : MassTransitStateMachine<VacationState>
    {
        public VacationStateMachine()
        {
            Event(() => VacationBookingProcessStarted, x => x.CorrelateById(m => m.Message.DealId));

            InstanceState(x => x.State);

            Initially(
                When(VacationBookingProcessStarted)
                    .Then(context => 
                    {
                        context.Instance.Updated = DateTime.UtcNow;
                        context.Instance.CustomerId = context.Data.CustomerId;
                        context.Instance.DealId = context.Data.DealId;
                    })
                    .TransitionTo(ProcessingStarted));
        }

        public State ProcessingStarted { get; set; }
        public State ProcessingCompleted { get; set; }

        public Event<IStartVacationBookingProcess> VacationBookingProcessStarted { get; set; }
    }
}
