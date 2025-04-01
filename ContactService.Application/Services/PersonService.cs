using ContactService.Core.Interfaces.Repository;
using ContactService.Core.Interfaces.Services;
using ContactService.Core.Models;
using AutoMapper;
using ContactService.Core.DTOs.PersonDtos;

namespace ContactService.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<Person>> AddPersonAsync(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _personRepository.AddPersonAsync(person);
            return BaseResponse<Person>.SuccessResponse(person, "Person added successfully.");

        }

        public async Task<BaseResponse<List<PersonGetAllResponse>>> GetAllAsync()
        {
            var people = await _personRepository.GetAllAsync();
            var response = _mapper.Map<List<PersonGetAllResponse>>(people);
            return BaseResponse<List<PersonGetAllResponse>>.SuccessResponse(response, "People retrieved successfully.");

        }
        public async Task<BaseResponse<bool>> DeletePersonAsync(Guid personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                return BaseResponse<bool>.ErrorResponse("Person not found");
            }

            await _personRepository.DeleteAsync(person);
            return BaseResponse<bool>.SuccessResponse(true, "Person deleted successfully");
        }

        public async Task<BaseResponse<PersonDetailsResponse>> GetPersonDetailsAsync(Guid personId)
        {
            var person = await _personRepository.GetDetailedByIdAsync(personId);
            if (person == null)
            {
                return BaseResponse<PersonDetailsResponse>.ErrorResponse("Person not found");
            }

            var personDetailsDto = _mapper.Map<PersonDetailsResponse>(person);

            return BaseResponse<PersonDetailsResponse>.SuccessResponse(personDetailsDto, "People detailed retrievde successfully.");

        }
        public async Task<BaseResponse<int>> GetPersonCountByLocationAsync(string location)
        {
            var result = await _personRepository.CountPersonsByLocationAsync(location);
            return BaseResponse<int>.SuccessResponse(result, "GetPersonCountByLocation retrievde successfully.");
        }

        public async Task<BaseResponse<int>> GetPhoneNumberCountByLocationAsync(string location)
        {
            var result=await _personRepository.CountPhoneNumbersByLocationAsync(location);
            return BaseResponse<int>.SuccessResponse(result, "GetPhoneNumberCountByLocation retrievde successfully.");

        }

        public async Task<PersonDto> GetPersonByIdAsync(Guid personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
                return null;

            return _mapper.Map<PersonDto>(person);
        }
    }
}
