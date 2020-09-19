using GreenPipes.Internals.Extensions;
using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.Events.HotelBooking;
using System.Threading.Tasks;

namespace TravelAgency.Components.CourierActivities.BookHotelActivity
{
    public class BookHotelActivity : IActivity<IBookHotelActivityArguments, IBookHotelActivityLog>
    {
        private readonly IRequestClient<IBookHotel> _hotelBookingClient;
        private readonly ILogger<BookHotelActivity> _logger;

        public BookHotelActivity(
            IRequestClient<IBookHotel> hotelBookingClient,
            ILogger<BookHotelActivity> logger)
        {
            this._hotelBookingClient = hotelBookingClient;
            this._logger = logger;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<IBookHotelActivityArguments> context)
        {
            this._logger.LogInformation($"Executing {nameof(BookHotelActivity)} activity" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

            var (accepted, rejected) = await this._hotelBookingClient.GetResponse<IHotelBookingAccepted, IHotelBookingRejected>(new
            {
                context.Arguments.HotelId,
                context.Arguments.RoomId,
                context.Arguments.CheckIn,
                context.Arguments.CheckOut,
            });

            if (accepted.IsCompletedSuccessfully())
            {
                var result = await accepted;

                return context.Completed<IBookHotelActivityLog>(new
                {
                    result.Message.BookingId
                });
            }
            else
            {
                var result = await rejected;

                this._logger.LogWarning($"Execution of  {nameof(BookHotelActivity)} activity failded. " +
                    $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}" +
                    $"\r\nReason: {result.Message.Reason}");

                return context.Faulted();
            }
        }
        
        public Task<CompensationResult> Compensate(CompensateContext<IBookHotelActivityLog> context)
        {
            this._logger.LogWarning($"Compensating {nameof(BookHotelActivity)} activity" +
                $"\r\nCompensation log: {JsonConvert.SerializeObject(context.Log)}");

            return Task.FromResult(context.Compensated());
        }
    }
}
