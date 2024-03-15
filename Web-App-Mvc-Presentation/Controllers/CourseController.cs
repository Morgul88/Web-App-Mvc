using Microsoft.AspNetCore.Mvc;

namespace Web_App_Mvc_Presentation.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult CourseView()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
