using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ReportService.Core.Interfaces;
using ReportService.Core.Models;

namespace ReportService.Tests.MockRepository
{
    public class MockReportRepository
    {
        public static Mock<IReportRepository> GetReportRepositoryMock()
        {
            var mockRepo = new Mock<IReportRepository>();

            mockRepo.Setup(repo => repo.GetReportsAsync())
                .ReturnsAsync(new List<Report>
                {
                    new Report
                {
                   Id = Guid.NewGuid(),
                   Location = "New York",
                   PersonCount = 10,
                   PhoneNumberCount = 15,
                   Status = "Completed",
                   CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                 new Report
                 {
                     Id = Guid.NewGuid(),
                     Location = "Los Angeles",
                     PersonCount = 20,
                     PhoneNumberCount = 30,
                     Status = "Pending",
                     CreatedAt = DateTime.UtcNow
                 }
             });

            return mockRepo;
        }
    }
}
