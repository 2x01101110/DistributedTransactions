using Automatonymous;
using MassTransit;
using TravelAgency.Components.StateMachines.VacationBooking.Activities;
using TravelAgency.Contracts.Events;
using TravelAgency.Contracts.Masstransit.Events;
using TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest;

namespace TravelAgency.Components.StateMachines.VacationBooking
{
    public class VacationBookingProcessStateMachine : MassTransitStateMachine<VacationBookingProcessState>
    {
        public VacationBookingProcessStateMachine()
        {
            
            Event(() => VacationBookingProcessStarted, x => x.CorrelateById(m => m.Message.VacationId));
            Event(() => VacationBookingProcessStateRequest, x =>
            {
                x.CorrelateById(m => m.Message.VacationId);
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<IVacationBookingProcessStateNotFound>(new
                        {
                            context.Message.VacationId
                        });
                    }
                }));
            });
            Event(() => VacationBookingFulfilled, x => x.CorrelateById(m => m.Message.VacationId));

            InstanceState(x => x.State);

            Initially(
                When(VacationBookingProcessStarted)
                    .Activity(x => x.OfType<VacationBookingProcessActivity>())
                    .TransitionTo(Processing));

            During(Processing, 
                Ignore(VacationBookingProcessStarted),
                When(VacationBookingFulfilled)
                    .TransitionTo(ProcessingCompleted));

            DuringAny(
                When(VacationBookingProcessStateRequest)
                .RespondAsync(x => x.Init<IVacationBookingProcessState>(new
                {
                    x.Instance.VacationId,
                    x.Instance.State
                })));
        }

        public State Processing { get; set; }
        public State ProcessingCompleted { get; set; }

        public Event<IVacationBookingProcessStarted> VacationBookingProcessStarted { get; set; }
        public Event<IVacationBookingProcessStateRequest> VacationBookingProcessStateRequest { get; set; }
        public Event<IVacationBookingFulfillmentCompleted> VacationBookingFulfilled { get; set; }
    }
}
