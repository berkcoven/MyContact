using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.DTOs;
using ContactService.Core.Models;

namespace ContactService.Core.Interfaces.Repository
{
    public interface IPersonRepository
    {
        Task<Person> AddPersonAsync(Person person);
        Task<List<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(Guid personId);
        Task DeleteAsync(Person person);
        Task<Person?> GetDetailedByIdAsync(Guid personId);
        Task<int> CountPersonsByLocationAsync(string location);
        Task<int> CountPhoneNumbersByLocationAsync(string location);
    }
}
