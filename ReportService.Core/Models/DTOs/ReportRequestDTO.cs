using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Core.Models.DTOs
{
    public class ReportRequestDTO
    {
        public string Location { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
