﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Core.Models.DTOs
{
    public class GetAllReportsResponseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}
