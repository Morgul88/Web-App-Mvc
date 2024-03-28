using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_App_Mvc_Presentation.ViewModels;

namespace Web_App_Mvc_Presentation.Controllers;


public class CoursesController(HttpClient httpClient) : Controller
{

    private readonly HttpClient _httpClient = httpClient;

    [HttpGet]
    [Route("/Courses")]
    public async  Task<IActionResult> CourseView()
    {
        var viewModel = new CoursesInfoViewModel();

        var response = await _httpClient.GetAsync("https://localhost:7070/api/Courses");
        
        viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;

        return View(viewModel);
    }
}
