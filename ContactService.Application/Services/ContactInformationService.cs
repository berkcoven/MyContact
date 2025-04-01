using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactService.Core.DTOs.ContactInfoDtos;
using ContactService.Core.Interfaces.Repository;
using ContactService.Core.Interfaces.Services;
using ContactService.Core.Models;

namespace ContactService.Application.Services
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ContactInformationService(IContactInformationRepository contactInformationRepository, IPersonRepository personRepository, IMapper mapper)
        {
            _contactInformationRepository = contactInformationRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

     
        public async Task<ContactInformation> AddContactInfoAsync(AddContactInfoDto addContactInfo)
        {
            if (addContactInfo == null)
            {
                throw new ArgumentException("Contact Information cannot be null.");
            }
            var personExists = await _personRepository.GetByIdAsync(addContactInfo.PersonId);
            if (personExists == null)
            {
                throw new ArgumentException("Person not found with the given ID");
            }
            var contactInfo = _mapper.Map<ContactInformation>(addContactInfo);
            return await _contactInformationRepository.AddAsync(contactInfo);
        }

        public async Task DeleteContactInformationAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }

            var existingContact = await _contactInformationRepository.GetByIdAsync(id);
            if (existingContact == null)
            {
                throw new KeyNotFoundException($"No ContactInformation found with Id: {id}");
            }

            await _contactInformationRepository.DeleteAsync(id);
        }
    }
}
