using ContactService.Application.Services;
using ContactService.Core.DTOs.ContactInfoDtos;
using ContactService.Core.Interfaces.Services;
using ContactService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationController : ControllerBase
    {
        private readonly IContactInformationService _contactInformationService;

        public ContactInformationController(IContactInformationService contactInformationService)
        {
            _contactInformationService = contactInformationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContactInformation([FromBody] AddContactInfoDto contactInformation)
        {
            if (contactInformation == null)
            {
                return BadRequest("Invalid contact information.");
            }

            var result = await _contactInformationService.AddContactInfoAsync(contactInformation);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactInformation(Guid id)
        {
            await _contactInformationService.DeleteContactInformationAsync(id);
            return NoContent();
        }


    }
}
