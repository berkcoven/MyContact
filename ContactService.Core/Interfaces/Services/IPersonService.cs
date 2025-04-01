using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.DTOs.PersonDtos;
using ContactService.Core.Models;

namespace ContactService.Core.Interfaces.Services
{
    public interface IPersonService
    {
        Task<BaseResponse<Person>> AddPersonAsync(PersonDto person);
        Task<BaseResponse<List<PersonGetAllResponse>>> GetAllAsync();
        Task<BaseResponse<bool>> DeletePersonAsync(Guid personId);
        Task<BaseResponse<PersonDetailsResponse>> GetPersonDetailsAsync(Guid personId);
        Task<BaseResponse<int>> GetPersonCountByLocationAsync(string location);
        Task<BaseResponse<int>> GetPhoneNumberCountByLocationAsync(string location);
        Task<PersonDto> GetPersonByIdAsync(Guid personId);


    }
}
