using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.Models;

namespace ContactService.Core.Interfaces.Repository
{
    public interface IContactInformationRepository
    {
        Task<ContactInformation> AddAsync(ContactInformation contactInformation); 
        Task DeleteAsync(Guid id);
        Task<ContactInformation> GetByIdAsync(Guid id);
    }
}
