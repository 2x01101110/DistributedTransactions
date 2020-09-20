using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TravelAgency.API.Models;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.BookVacation;
using TravelAgency.Contracts.Commands.SharedCommandContracts;
using TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest;
using TravelAgency.Models;

namespace TravelAgency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        readonly IRequestClient<IVacationBookingProcessStateRequest> _vacationBookingProcessClient;
        readonly IRequestClient<IBookVacation> _bookVacationClient;

        public ReservationsController(
            IRequestClient<IVacationBookingProcessStateRequest> vacationBookingProcessClient,
            IRequestClient<IBookVacation> bookVacationClient)
        {
            this._vacationBookingProcessClient = vacationBookingProcessClient;
            this._bookVacationClient = bookVacationClient;
        }

        [HttpGet("{vacationId}")]
        public async Task<IActionResult> GetReservationStatus([FromRoute]Guid vacationId)
        {
            var (status, notFound) = await this._vacationBookingProcessClient
                .GetResponse<IVacationBookingProcessState, IVacationBookingProcessStateNotFound>(new
                {
                    VacationId = vacationId
                });

            if (status.IsCompletedSuccessfully)
            {
                var result = await status;
                return Ok(result.Message);
            }
            else
            {
                var result = await notFound;
                return NotFound(result.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody]VacationBookingModel model)
        {
            var (accepted, rejected) = await this._bookVacationClient
                .GetResponse<IVacationBookingProcessAccepted, IVacationBookingProcessRejected>(new
                {
                    model.DealId,
                    model.CustomerId,
                    VacationExtras = (IVacationExtras)model.VacationExtras
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
