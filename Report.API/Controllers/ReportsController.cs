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
        public async Task<ActionResult<Guid>> ReportRequest()
        {
            var result = await _reportService.CreateNewReport();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReportDto>>> GetAllReports()
        {
            var result = await _reportService.GetAllReports();
            return Ok(result);
        }
    }
}
