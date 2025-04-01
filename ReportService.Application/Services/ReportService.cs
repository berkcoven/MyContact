using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ReportService.Core.Interfaces;
using ReportService.Core.Models;
using ReportService.Core.Models.DTOs;
using ReportService.Infrastructure.Consumer.Services;

namespace ReportService.Application.Services
{
    public class ReportService:IReportService
    {
        private readonly IContactServiceClient _contactServiceClient;
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IContactServiceClient contactServiceClient, IReportRepository reportRepository,IMapper mapper)
        {
            _contactServiceClient = contactServiceClient;
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllReportsResponseDto>> GetAllReportsAsync()
        {

            var result= await _reportRepository.GetReportsAsync();
                
            return _mapper.Map<IEnumerable<GetAllReportsResponseDto>>(result);
        }

        public async Task<Report> GetReportByIdAsync(Guid id)
        {
            return await _reportRepository.GetReportByIdAsync(id);
        }

        public async Task ProcessReportRequestAsync(ReportRequest reportRequest)
        {
            var data = await _contactServiceClient.GetPersonAndPhoneCountByLocationAsync(reportRequest.Location);
            var report = new Report
            {
                Location = reportRequest.Location,
                PersonCount = data.reportResponseDto.PersonCount,
                PhoneNumberCount = data.reportResponseDto.PersonCount,
                CreatedAt = DateTime.UtcNow,
                Status ="Pending"
            };
            await _reportRepository.AddReportAsync(report);
            Console.WriteLine($"Location: {reportRequest.Location}, Persons: {data.reportResponseDto.PersonCount}, Phones: {data.reportResponseDto.PersonCount}");

        }
    }
}
