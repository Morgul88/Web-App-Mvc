using Microsoft.AspNetCore.Mvc;

namespace Web_App_Mvc_Presentation.Controllers
{
    public class SubscribersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
