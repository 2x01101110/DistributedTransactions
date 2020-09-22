using GreenPipes.Internals.Extensions;
using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.CarRental;
using Shared.Contracts.CarRenting;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TravelAgency.Components.CourierActivities.RentCarActivity
{
    public class RentCarActivity : IActivity<IRentCarActivityArguments, IRentCarActivityLog>
    {
        private readonly IRequestClient<IRentCar> _rentCarClient;
        private readonly ILogger<RentCarActivity> _logger;

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
        
        public async Task<CompensationResult> Compensate(CompensateContext<IRentCarActivityLog> context)
        {
            this._logger.LogWarning($"Compensating {nameof(RentCarActivity)} activity" +
                $"\r\nCompensation log: {JsonConvert.SerializeObject(context.Log)}");

            await context.Publish<ICancelCarRental>(new
            {
                context.Log.CarRentalId
            });

            return context.Compensated();
        }
    }
}
