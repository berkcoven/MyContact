using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ReportService.Core.Models;
using ReportService.Core.Models.DTOs;


namespace ReportService.Infrastructure.Mapping
{
    public class ReportRequestProfile : Profile
    {
        public ReportRequestProfile()
        {
            CreateMap<ReportRequestDTO, ReportRequest>();
            CreateMap<Report, GetAllReportsResponseDto>();
           
        }
    }
}
