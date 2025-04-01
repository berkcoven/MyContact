using ContactService.Core.DTOs.ReportDtos;
using ContactService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContactService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IRabbitMqProducerService _rabbitMqProducerService;

        public ReportController(IRabbitMqProducerService rabbitMqProducerService)
        {
            _rabbitMqProducerService = rabbitMqProducerService;
        }

        [HttpPost("request-report")]
        public IActionResult RequestReport([FromBody] ReportRequestDto request)
        {
            var message = JsonConvert.SerializeObject(request);
            _rabbitMqProducerService.PublishMessage(message);
            return Ok(new { Message = "Report request has been sent to the queue." });
        }
    }

}
