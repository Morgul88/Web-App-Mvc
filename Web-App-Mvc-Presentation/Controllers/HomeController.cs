using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
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

    [HttpPost]
    [Route("/subscriber")]
    public async Task<IActionResult> Subscribe(SubscriberEntity model)
    {
        ViewData["Success subscribed"] = false;
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            using var content = new StringContent(json, Encoding.UTF8, "application/json" );
            var response = await http.PostAsync("https://localhost:7070/api/Courses/Subscribers", content);
            if(response.IsSuccessStatusCode)
            {
                ViewData["Success subscribed"] = true;
            }
            
        }
        return View();

    }
}
