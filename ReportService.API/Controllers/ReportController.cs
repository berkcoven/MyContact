using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Core.Interfaces;
using ReportService.Core.Models;
using ReportService.Core.Models.DTOs;

namespace ReportService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public IActionResult CreateReport([FromBody] ReportRequestDTO reportRequestDto)
        {
        
            var reportRequest = new ReportRequest
            {
                Location = reportRequestDto.Location,
                RequestedAt = reportRequestDto.RequestedAt
            };

            _reportService.ProcessReportRequestAsync(reportRequest);

            return Ok("Rapor oluşturuluyor...");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReportById(Guid id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
                return NotFound();

            return Ok(report);
        }

    }
}
