using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.Core.Models;
using ReportService.Core.Models.DTOs;

namespace ReportService.Core.Interfaces
{
    public interface IReportService
    {
        Task ProcessReportRequestAsync(ReportRequest reportRequest);
        Task<IEnumerable<GetAllReportsResponseDto>> GetAllReportsAsync();
        Task<Report> GetReportByIdAsync(Guid id);
    }
}
