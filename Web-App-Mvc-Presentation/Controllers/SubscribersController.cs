using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_App_Mvc_Presentation.Models;

namespace Web_App_Mvc_Presentation.Controllers
{
    public class SubscribersController : Controller
    {
       

        [Route("/subscriber")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Success subscribed"] = false;
            return View(new SubscribeModel());
        }


        [HttpPost]
        [Route("/subscriber")]
        public async Task<IActionResult> Index(SubscribeModel model)
        {
            
            if (ModelState.IsValid)
            {
                using var http = new HttpClient();
                
                var apiKey = "M2FiNzYzMzQtMWJkNi00ODRmLTg1NzQtNjlmOGFmNzE1Yzdh";
                var url = $"https://localhost:7070/api/Subscribers?email={model.Email}&Key={apiKey}";
                
                var request = new HttpRequestMessage(HttpMethod.Post, url) ;

                var response = await http.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "You have successfully subscribed to our newsletter!";
                    return RedirectToAction("Index", "Home", new { fragment = "dont-miss-anything" });
                }

                else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
                {
                    ViewData["Status"] = "Unauthorized";
                    TempData["SuccessMessage"] = "You are unauthorized. Please contact admin";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "Already Exist";
                    TempData["SuccessMessage"] = "You have already subscribed to our news";
                }

            }
            return RedirectToAction("Index", "Home");

        }
    }
}
