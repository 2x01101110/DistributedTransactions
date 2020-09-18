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
                this._logger.LogInformation($"Publishing {nameof(IVacationBookingProcessStarted)} event" +
                    $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

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
                this._logger.LogError($"An error occured in {nameof(IBookVacation)} consumer" +
                    $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}" +
                    $"\r\nError: {ex.Message} {ex.StackTrace}");

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
