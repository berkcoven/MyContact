using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactService.Core.DTOs.ReportDtos
{
    public class ReportRequestDto
    {
        public string Location { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; }= DateTime.Now;
    }

}
