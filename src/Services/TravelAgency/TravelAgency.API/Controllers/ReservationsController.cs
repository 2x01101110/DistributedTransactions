using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelAgency.API.Models;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.BookVacation;
using TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest;

namespace TravelAgency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        readonly IRequestClient<IVacationBookingProcessStateRequest> _vacationBookingProcessClient;
        readonly IRequestClient<IBookVacation> _bookVacationClient;
        readonly ILogger<ReservationsController> _logger;

        public ReservationsController(
            IRequestClient<IVacationBookingProcessStateRequest> vacationBookingProcessClient,
            IRequestClient<IBookVacation> bookVacationClient,
            ILogger<ReservationsController> logger)
        {
            this._vacationBookingProcessClient = vacationBookingProcessClient;
            this._bookVacationClient = bookVacationClient;
            this._logger = logger;
        }

        [HttpGet("{dealId}")]
        public async Task<IActionResult> GetReservationStatus([FromRoute]Guid dealId)
        {
            var (status, notFound) = await this._vacationBookingProcessClient
                .GetResponse<IVacationBookingProcessState, IVacationBookingProcessStateNotFound>(new
                {
                    DealId = dealId
                });

            if (status.IsCompletedSuccessfully)
            {
                var result = await status;
                return NotFound(result.Message);
            }
            else
            {
                var result = await notFound;
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody]BookVacationModel model)
        {
            // Send IBookVacation book command to consumer
            var (accepted, rejected) = await this._bookVacationClient
                .GetResponse<IVacationBookingProcessAccepted, IVacationBookingProcessRejected>(new
                {
                    model.DealId,
                    model.CustomerId
                });

            if (accepted.IsCompletedSuccessfully)
            {
                var result = await accepted;
                return Ok(result.Message);
            }
            else
            {
                var result = await rejected;
                return BadRequest(result.Message);
            }
        }
    }
}
