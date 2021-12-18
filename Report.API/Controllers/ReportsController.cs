using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.API.Dto;
using Report.API.Services;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("Request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> ReportRequest()
        {
            var result = await _reportService.CreateNewReport();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ReportDto>>> GetAllReports()
        {
            var result = await _reportService.GetAllReports();
            return Ok(result);
        }

        [HttpGet("{reportId}/Detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReportDetailDto>> GetReportDetail(Guid reportId)
        {
            var result = await _reportService.GetReportDetail(reportId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
