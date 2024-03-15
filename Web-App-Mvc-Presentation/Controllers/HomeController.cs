using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_App_Mvc_Presentation.Models;
using Web_App_Mvc_Presentation.ViewModels;

namespace Web_App_Mvc_Presentation.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        
        return View();
        
    }

    [Route("/error")]
    public IActionResult Error404()
    {
        return View();
    }

    [HttpGet]
    [Route("/contacts")]
    public IActionResult Contacts()
    {

        return View();

    }
}
