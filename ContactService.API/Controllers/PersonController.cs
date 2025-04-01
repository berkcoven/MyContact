using System.Net;
using ContactService.Application.Services;
using ContactService.Core.DTOs.PersonDtos;
using ContactService.Core.DTOs.ReportDtos;
using ContactService.Core.Interfaces.Services;
using ContactService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var people = await _personService.GetAllAsync();

            if (!people.Success)
            {
                return NotFound("No people found.");
            }
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonDetails(Guid id)
        {
            var person = await _personService.GetPersonDetailsAsync(id);
            if (person == null)
            {
                return NotFound("Person not found.");
            }
            return Ok(person);
        }
        [HttpGet("count-by-location/{location}")]
        public async Task<IActionResult> GetPersonAndPhoneCountByLocation(string location)
        {
            var personCount = await _personService.GetPersonCountByLocationAsync(location);
            var phoneNumberCount = await _personService.GetPhoneNumberCountByLocationAsync(location);
            if (personCount.Success && phoneNumberCount.Success)
            {
                ReportResponseDto reportResponseDto = new ReportResponseDto { PersonCount=personCount.Data,PhoneNoCount=phoneNumberCount.Data};
                return Ok(new { reportResponseDto });
            }
            return BadRequest("Couldnt retrieve person or phone data");
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] PersonDto person)
        {
            if (person == null)
                return BadRequest("Person object cannot be null");

            var addedPerson = await _personService.AddPersonAsync(person);
            if (!addedPerson.Success)
            {
                return BadRequest(addedPerson);
            }
            return Ok();
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePerson(Guid personId)
        {
            var response = await _personService.DeletePersonAsync(personId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }




    }
}
