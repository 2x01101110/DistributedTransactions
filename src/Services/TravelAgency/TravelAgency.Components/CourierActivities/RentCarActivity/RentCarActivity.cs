using GreenPipes.Internals.Extensions;
using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.CarRenting;
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
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Arguments)}");

            var (accepted, rejected) = await this._rentCarClient.GetResponse<ICarRentalAccepted, ICarRentalRejected>(new
            {
                context.Arguments.CarId,
                context.Arguments.RentFrom,
                context.Arguments.RentTo
            });

            var test = accepted.IsCompletedSuccessfully();

            if (accepted.IsCompletedSuccessfully())
            {
                var result = await accepted;

                return context.Completed<IRentCarActivityLog>(new
                {
                    result.Message.CarRentalId
                });
            }
            else {
                var result = await rejected;

                return context.Faulted();
            }
        }
        
        public Task<CompensationResult> Compensate(CompensateContext<IRentCarActivityLog> context)
        {
            this._logger.LogWarning($"Compensating {nameof(RentCarActivity)} activity" +
                $"\r\nCompensation log: {JsonConvert.SerializeObject(context.Log)}");

            return Task.FromResult(context.Compensated());
        }
    }
}
