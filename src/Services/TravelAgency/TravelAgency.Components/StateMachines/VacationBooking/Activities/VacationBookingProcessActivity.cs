using Automatonymous;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.FulfillVacationBooking;
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
            this._logger.LogInformation($"Executing ${nameof(VacationBookingProcessActivity)} activity" +
                $"\r\nPayload: ${JsonConvert.SerializeObject(context.Data)}");

            context.Instance.VacationId = context.Data.VacationId;
            context.Instance.VacationStart = context.Data.VacationStart;
            context.Instance.VacationEnd = context.Data.VacationEnd;
            context.Instance.DealId = context.Data.DealId;
            context.Instance.CustomerId = context.Data.CustomerId;
            context.Instance.Hotel = new Hotel
            {
                HotelId = context.Data.Hotel.HotelId,
                RoomId = context.Data.Hotel.RoomId
            };
            context.Instance.TravelClass = context.Data.TravelClass;
            context.Instance.CarId = context.Data.CarId;
            context.Instance.Updated = DateTime.UtcNow;

            var consumeContext = context.GetPayload<ConsumeContext>();
            var sendEndpoint = await consumeContext.GetSendEndpoint(new Uri("queue:fulfill-vacation-booking"));

            await sendEndpoint.Send<IFulfillVacationBooking>(new
            {
                context.Instance.VacationId,
                context.Instance.VacationStart,
                context.Instance.VacationEnd,
                context.Instance.DealId,
                context.Instance.CustomerId,
                Hotel = new
                {
                    context.Instance.Hotel.HotelId,
                    context.Instance.Hotel.RoomId
                },
                context.Instance.TravelClass,
                context.Instance.CarId
            });

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(
            BehaviorExceptionContext<VacationBookingProcessState, IVacationBookingProcessStarted, TException> context, 
            Behavior<VacationBookingProcessState, IVacationBookingProcessStarted> next) where TException : Exception
        {
            var exception = context.Exception;

            this._logger.LogError($"Execution of {nameof(VacationBookingProcessActivity)} activity failed" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Data)}" +
                $"\r\nError: {exception.Message} {exception.StackTrace}");

            return next.Faulted(context);
        }
    }
}
