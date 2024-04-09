using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CourseService
    {
        public readonly HttpClient _httpClient;
        public readonly IConfiguration _configuration;

        public CourseService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<CourseResult> GetCoursesAsync(string category ="",string searchQuery = "", int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7070/api/Courses?key={_configuration["ApiKey:Secret"]}&category={Uri.EscapeDataString(category)}&searchQuery={Uri.EscapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());
                if(result != null && result.Succeeded)
                {
                    return result;
                }
            }
            return null!;
        }
    }
}
