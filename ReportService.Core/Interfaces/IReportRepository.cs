using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.Core.Models;

namespace ReportService.Core.Interfaces
{
    public interface IReportRepository
    {
        Task AddReportAsync(Report report);
        Task<List<Report>> GetReportsAsync();
        Task<Report> GetReportByIdAsync(Guid reportId);
    }
}
