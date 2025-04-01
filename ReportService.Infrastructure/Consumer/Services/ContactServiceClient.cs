using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ReportService.Core.Interfaces;
using ReportService.Core.Models.DTOs;

namespace ReportService.Infrastructure.Consumer.Services
{
    public class ContactServiceClient : IContactServiceClient
    {
        private readonly HttpClient _httpClient;

        public ContactServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5157"); 
        }

        public async Task<ReportResponseDto> GetPersonAndPhoneCountByLocationAsync(string location)
        {
            var response = await _httpClient.GetFromJsonAsync<ReportResponseDto>($"api/Person/count-by-location/{location}");
            return response ?? throw new Exception("No data received.");
        }
    }
}
