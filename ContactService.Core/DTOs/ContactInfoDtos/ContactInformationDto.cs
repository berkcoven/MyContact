using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Core.DTOs.ContactInfoDtos
{
    public class ContactInformationDto
    {
        public Guid Id { get; set; }
        public string InfoType { get; set; }  
        public string InfoContent { get; set; }
    }
}
