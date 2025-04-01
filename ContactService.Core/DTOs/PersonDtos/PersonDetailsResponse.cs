using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.DTOs.ContactInfoDtos;

namespace ContactService.Core.DTOs.PersonDtos
{
    public class PersonDetailsResponse
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

        public List<ContactInformationDto> ContactInformations { get; set; }
    }
}
