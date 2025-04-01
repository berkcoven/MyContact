using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReportService.Core.Models.DTOs
{
    public class ReportResponseDto
    {
        public PersonPhoneCountDto reportResponseDto { get; set; }
    }
}
