using MassTransit.Courier;
using System.Threading.Tasks;

namespace TravelAgency.Components.CourierActivities.RentCarActivity
{
    public class RentCarActivity : IActivity<IRentCarActivityArguments, IRentCarActivityLog>
    {
        public Task<ExecutionResult> Execute(ExecuteContext<IRentCarActivityArguments> context)
        {
            return Task.FromResult(context.Completed(new { }));
        }
        
        public Task<CompensationResult> Compensate(CompensateContext<IRentCarActivityLog> context)
        {
            return Task.FromResult(context.Compensated());
        }
    }
}
