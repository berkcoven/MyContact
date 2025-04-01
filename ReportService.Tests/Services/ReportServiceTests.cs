using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ReportService.Core.Interfaces;
using ReportService.Application.Services;
using ReportService.Tests.MockRepository;
using ReportService.Infrastructure.Consumer.Services;
using AutoMapper;
using ReportService.Core.Models.DTOs;
using ReportService.Core.Models;
using FluentAssertions;

namespace ReportService.Tests.Services
{
    public class ReportServiceTests
    {
        private readonly Mock<IReportRepository> _mock;
        private readonly IReportService _reportService;
        private readonly IContactServiceClient _contactServiceClient;
        private readonly Mock<IMapper> _mockMapper;
        public ReportServiceTests()
        {
            _mock = MockReportRepository.GetReportRepositoryMock();
            _mockMapper = new Mock<IMapper>();
            _contactServiceClient = new ContactServiceClient(new HttpClient());
            _reportService = new ReportService.Application.Services.ReportService(_contactServiceClient,_mock.Object, _mockMapper.Object);

        }

        [Fact]
        public async Task GetAllReportsAsync_ShouldReturnReports_WhenReportsExist()
        {
            // Arrange: Test verisi oluştur
            var reports = new List<Report>
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
    };

            var reportDtos = new List<GetAllReportsResponseDto>
    {
        new GetAllReportsResponseDto
        {
            Id = reports[0].Id,
            Location = reports[0].Location,
            Status = reports[0].Status,
            CreatedAt = reports[0].CreatedAt
        },
        new GetAllReportsResponseDto
        {
            Id = reports[1].Id,
            Location = reports[1].Location,
            Status = reports[1].Status,
            CreatedAt = reports[1].CreatedAt
        }
    };

            _mock.Setup(repo => repo.GetReportsAsync()).ReturnsAsync(reports);
            _mockMapper.Setup(m => m.Map<IEnumerable<GetAllReportsResponseDto>>(reports)).Returns(reportDtos);

            // Act: GetAllReportsAsync metodunu çalıştır
            var result = await _reportService.GetAllReportsAsync();

            // Assert: Sonuçların doğru olduğunu doğrula
            result.Should().NotBeNull();
            result.Count().Should().Be(2); // İki rapor döndürmeli
            result.First().Location.Should().Be("New York");  // İlk raporun konumu "New York" olmalı
            result.Last().Status.Should().Be("Pending"); // Son raporun durumu "Pending" olmalı
        }

        [Fact]
        public async Task GetReportByIdAsync_ShouldReturnReport_WhenReportExists()
        {
            // Arrange: Test verisi oluştur
            var reportId = Guid.NewGuid();
            var report = new Report
            {
                Id = reportId,
                Location = "Chicago",
                PersonCount = 50,
                PhoneNumberCount = 60,
                Status = "Completed",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            };

            var reportDto = new Report
            {
                Id = report.Id,
                Location = report.Location,
                PersonCount = report.PersonCount,
                PhoneNumberCount = report.PhoneNumberCount,
                Status = report.Status,
                CreatedAt = report.CreatedAt
            };

            _mock.Setup(repo => repo.GetReportByIdAsync(reportId)).ReturnsAsync(report);
            _mockMapper.Setup(m => m.Map<Report>(report)).Returns(reportDto);

            // Act: GetReportByIdAsync metodunu çalıştır
            var result = await _reportService.GetReportByIdAsync(reportId);

            // Assert: Sonuçların doğru olduğunu doğrula
            result.Should().NotBeNull();
            result.Id.Should().Be(reportId); // Dönen raporun Id'si doğru olmalı
            result.Location.Should().Be("Chicago"); // Konum doğru olmalı
            result.Status.Should().Be("Completed"); // Durum doğru olmalı
            result.PersonCount.Should().Be(50); // PersonCount doğru olmalı
        }


    }
}
