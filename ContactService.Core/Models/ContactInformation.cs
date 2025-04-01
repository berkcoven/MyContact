using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactService.Core.Models
{

    public class ContactInformation
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public InfoTypeEnum InfoType { get; set; }
        public string InfoContent { get; set; }
        [JsonIgnore]
        public Person Person { get; set; }
    }
}


