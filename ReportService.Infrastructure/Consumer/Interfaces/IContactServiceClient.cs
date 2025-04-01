using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.Core.Models.DTOs;

namespace ReportService.Core.Interfaces
{
    public interface IContactServiceClient
    {
        Task<ReportResponseDto> GetPersonAndPhoneCountByLocationAsync(string location);
    }

}
