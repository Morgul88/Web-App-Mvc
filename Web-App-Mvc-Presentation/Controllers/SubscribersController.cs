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

                var url = $"https://localhost:7070/api/Subscribers?email={model.Email}";

                var request = new HttpRequestMessage(HttpMethod.Post, url) ;

                var response = await http.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "You have successfully subscribed to our newsletter!";
                    return RedirectToAction("Index", "Home", new { fragment = "dont-miss-anything" });
                }

            }
            return RedirectToAction("Index", "Home");

        }
    }
}
