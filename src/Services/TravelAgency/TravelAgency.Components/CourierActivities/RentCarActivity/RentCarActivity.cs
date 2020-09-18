using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.Events;
using System;
using System.Threading.Tasks;

namespace TravelAgency.Components.CourierActivities.RentCarActivity
{
    public class RentCarActivity : IActivity<IRentCarActivityArguments, IRentCarActivityLog>
    {
        private readonly ILogger<RentCarActivity> _logger;
        private readonly IRequestClient<IRentCar> _rentCarClient;

        public RentCarActivity(
            IRequestClient<IRentCar> rentCarClient,
            ILogger<RentCarActivity> logger)
        {
            this._rentCarClient = rentCarClient;
            this._logger = logger;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<IRentCarActivityArguments> context)
        {
            this._logger.LogInformation($"Executing {nameof(RentCarActivity)} activity" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

            var response = await this._rentCarClient.GetResponse<ICarRented>(new
            {
                context.Arguments.CarId,
                context.Arguments.RentFrom,
                context.Arguments.RentTo
            });

            return context.Completed(new 
            {
                context.Arguments.CarId
            });
        }
        
        public Task<CompensationResult> Compensate(CompensateContext<IRentCarActivityLog> context)
        {
            this._logger.LogWarning($"Compensating {nameof(RentCarActivity)} activity" +
                $"\r\nCompensation log: {JsonConvert.SerializeObject(context.Log)}");

            return Task.FromResult(context.Compensated());
        }
    }
}
