using Microsoft.AspNetCore.Mvc;
using TelephoneConversations.Core.Interfaces;
using TelephoneConversations.Core.Models.DTOs;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("byCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityReportDTO>> GetReportByCities([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate > toDate)
            {
                return BadRequest("Дата початку не може бути пізнішою за дату кінця.");
            }

            var report = await _reportService.GetReportByCitiesAsync(fromDate, toDate);
            return Ok(report);
        }

        [HttpGet("bySubscribers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityReportDTO>> GetReportBySubscribers([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate > toDate)
            {
                return BadRequest("Дата початку не може бути пізнішою за дату кінця.");
            }

            var report = await _reportService.GetReportBySubscribersAsync(fromDate, toDate);
            return Ok(report);
        }
    }
}
