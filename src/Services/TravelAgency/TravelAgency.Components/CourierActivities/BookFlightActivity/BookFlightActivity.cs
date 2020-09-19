using GreenPipes.Internals.Extensions;
using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.FlightBooking;
using System;
using System.Threading.Tasks;

namespace TravelAgency.Components.CourierActivities.BookFlightActivity
{
    public class BookFlightActivity : IActivity<IBookFlightActivityArguments, IBookFlightActivityLog>
    {
        private readonly IRequestClient<IBookFlight> _flightBookingClient;
        private readonly ILogger<BookFlightActivity> _logger;

        public BookFlightActivity(
            IRequestClient<IBookFlight> flightBookingClient,
            ILogger<BookFlightActivity> logger)
        {
            this._flightBookingClient = flightBookingClient;
            this._logger = logger;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<IBookFlightActivityArguments> context)
        {
            this._logger.LogInformation($"Executing {nameof(BookFlightActivity)} activity" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

            var (accepted, rejected) = await this._flightBookingClient
                .GetResponse<IBookFlightAccepted, IBookFlightRejected>(new
                {
                    context.Arguments.AirportId,
                    context.Arguments.DepartureDate,
                    context.Arguments.DestinationId,
                    context.Arguments.ReturnDate,
                    context.Arguments.ReturnId
                });

            var test = accepted.IsCompletedSuccessfully();

            if (accepted.IsCompletedSuccessfully())
            {
                var result = await accepted;

                return context.Completed<IBookFlightActivityLog>(new
                {
                    result.Message.DepartureFlightId,
                    result.Message.ReturnFLightId
                });
            }
            else
            {
                var result = await rejected;

                return context.Faulted();
            }
        }

        public Task<CompensationResult> Compensate(CompensateContext<IBookFlightActivityLog> context)
        {
            this._logger.LogWarning($"Compensating {nameof(BookFlightActivity)} activity" +
                $"\r\nCompensation log: {JsonConvert.SerializeObject(context.Log)}");

            return Task.FromResult(context.Compensated());
        }
    }
}
