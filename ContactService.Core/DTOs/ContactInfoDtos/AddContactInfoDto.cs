using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.Models;

namespace ContactService.Core.DTOs.ContactInfoDtos
{
    public class AddContactInfoDto
    {
        public Guid PersonId { get; set; }
        public InfoTypeEnum InfoType { get; set; }  
        public string InfoContent { get; set; }  
    }
}
