using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web_App_Mvc_Presentation.ViewModels;

namespace Web_App_Mvc_Presentation.Controllers;


public class CoursesController(HttpClient httpClient, IConfiguration configuration) : Controller
{

    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    


    [HttpGet]
    [Route("/Courses")]
    public async Task<IActionResult> CourseView(string category = "")
    {
        if (HttpContext.Request.Cookies.TryGetValue("Accesstoken", out var token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CoursesInfoViewModel viewModel = new CoursesInfoViewModel();

            var response = await _httpClient.GetAsync($"https://localhost:7070/api/Courses?key={_configuration["ApiKey:Secret"]}&category={category}");

            if (response.IsSuccessStatusCode) 
            { 
                var data = await response.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(data);

                return View(courses);
            }

            
        };


        return View();
    }

    [HttpGet]
    [Route("/Course")]
    public async Task<IActionResult> OneCourseView(string id)
    {
        if (HttpContext.Request.Cookies.TryGetValue("Accesstoken", out var token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CoursesInfoViewModel viewModel = new CoursesInfoViewModel();

            var response = await _httpClient.GetAsync($"https://localhost:7070/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<CourseModel>(data);

                return View(courses);
            }


        };


        return View();
    }

}


