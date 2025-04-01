using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Core.Models
{
    public class ReportRequest
    {
        public string Location { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
