using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportService.Core.Interfaces;
using ReportService.Core.Models;
using ReportService.Infrastructure.Database;

namespace ReportService.Infrastructure.Repositories
{

    public class ReportRepository : IReportRepository
    {
        private readonly ReportDbContext _context;

        public ReportRepository(ReportDbContext context)
        {
            _context = context;
        }

        public async Task AddReportAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            report.Status = "Completed";
            await _context.SaveChangesAsync();
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(Guid reportId)
        {
            return await _context.Reports.FindAsync(reportId);
        }
    }
}
