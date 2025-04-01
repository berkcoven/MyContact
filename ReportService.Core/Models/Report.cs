using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Core.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public string Status { get; set; } 
    }

}
