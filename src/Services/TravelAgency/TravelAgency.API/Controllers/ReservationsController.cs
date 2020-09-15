using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelAgency.API.Models;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.BookVacation;

namespace TravelAgency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        readonly IRequestClient<IBookVacation> _bookVacationClient;
        readonly ILogger<ReservationsController> _logger;

        public ReservationsController(
            IRequestClient<IBookVacation> bookVacationClient,
            ILogger<ReservationsController> logger)
        {
            this._bookVacationClient = bookVacationClient;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody]BookVacationModel model)
        {
            // Send IBookVacation book command to consumer
            var (accepted, rejected) = await this._bookVacationClient
                .GetResponse<IBookVacationProcessAccepted, IBookVacationProcessRejected>(new
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
