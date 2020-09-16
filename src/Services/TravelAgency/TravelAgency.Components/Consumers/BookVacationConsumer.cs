using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.BookVacation;
using TravelAgency.Contracts.Masstransit.Events;

namespace TravelAgency.Components.Consumers
{
    public class BookVacationConsumer : IConsumer<IBookVacation>
    {
        private readonly ILogger<BookVacationConsumer> _logger;

        public BookVacationConsumer(ILogger<BookVacationConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IBookVacation> context)
        {
            try
            {
                this._logger.LogInformation($"Publishing {nameof(IVacationBookingProcessStarted)} event " +
                    $"with payload {JsonConvert.SerializeObject(context.Message)}");

                // Publish event indicating that vacation was booked
                await context.Publish<IVacationBookingProcessStarted>(new
                {
                    context.Message.DealId,
                    context.Message.CustomerId
                });

                await context.RespondAsync<IVacationBookingProcessAccepted>(new 
                { 
                    context.Message.DealId,
                    context.Message.CustomerId
                });
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{nameof(IBookVacation)} command failed to complete, reason:\r\n{ex.Message}");

                await context.RespondAsync<IVacationBookingProcessRejected>(new
                {
                    context.Message.DealId,
                    context.Message.CustomerId,
                    Reason = "Internal error occured"
                });
            }
        }
    }
}
