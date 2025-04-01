using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.DTOs.ContactInfoDtos;
using ContactService.Core.Models;

namespace ContactService.Core.Interfaces.Services
{
    public interface IContactInformationService
    {
        Task<ContactInformation> AddContactInfoAsync(AddContactInfoDto addContactInfo);
        Task DeleteContactInformationAsync(Guid id);
    }
}
