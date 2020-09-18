using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Components.Consumers
{
    public class RoutingSlipEventConsumer :
        IConsumer<RoutingSlipActivityCompleted>,
        IConsumer<RoutingSlipActivityFaulted>,
        IConsumer<RoutingSlipActivityCompensated>,
        IConsumer<RoutingSlipActivityCompensationFailed>
    {
        private readonly ILogger<RoutingSlipEventConsumer> _logger;

        public RoutingSlipEventConsumer(ILogger<RoutingSlipEventConsumer> logger)
        {
            this._logger = logger;
        }

        public Task Consume(ConsumeContext<RoutingSlipActivityCompleted> context)
        {
            this._logger.LogInformation($"Routingslip activity completed: {context.Message.TrackingNumber} {context.Message.ActivityName}");

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RoutingSlipActivityFaulted> context)
        {
            var exception = context.Message.ExceptionInfo;

            this._logger.LogError($"Routingslip activity faulted: {context.Message.TrackingNumber} {context.Message.ActivityName}\r\n" +
                $"Reason: {exception.ExceptionType}\r\n{exception.StackTrace}");

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RoutingSlipActivityCompensated> context)
        {
            this._logger.LogError($"Routingslip activity compensated: {context.Message.TrackingNumber} {context.Message.ActivityName}\r\n");

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RoutingSlipActivityCompensationFailed> context)
        {
            var exception = context.Message.ExceptionInfo;

            this._logger.LogError($"Routingslip activity compensation faulted: {context.Message.TrackingNumber} {context.Message.ActivityName}\r\n" +
                $"Reason: {exception.ExceptionType}\r\n{exception.StackTrace}");

            return Task.CompletedTask;
        }
    }
}
