using Automatonymous;
using GreenPipes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Contracts.Masstransit.Events;

namespace TravelAgency.Components.StateMachines.VacationBooking.Activities
{
    public class VacationBookingProcessActivity :
        Activity<VacationBookingProcessState, IVacationBookingProcessStarted>
    {
        private ILogger<VacationBookingProcessActivity> _logger;

        public VacationBookingProcessActivity(ILogger<VacationBookingProcessActivity> logger)
        {
            this._logger = logger;
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("start-booking-process");
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(
            BehaviorContext<VacationBookingProcessState, IVacationBookingProcessStarted> context, 
            Behavior<VacationBookingProcessState, IVacationBookingProcessStarted> next)
        {
            this._logger.LogInformation($"Starting execution of ${nameof(VacationBookingProcessActivity)} with payload " +
                $"${JsonConvert.SerializeObject(context.Data)}");

            context.Instance.Updated = DateTime.UtcNow;
            context.Instance.CustomerId = context.Data.CustomerId;
            context.Instance.DealId = context.Data.DealId;

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(
            BehaviorExceptionContext<VacationBookingProcessState, IVacationBookingProcessStarted, TException> context, 
            Behavior<VacationBookingProcessState, IVacationBookingProcessStarted> next) where TException : Exception
        {
            return next.Faulted(context);
        }
    }
}
