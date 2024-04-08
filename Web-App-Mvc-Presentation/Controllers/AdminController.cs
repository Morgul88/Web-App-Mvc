using Humanizer;
using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Web_App_Mvc_Presentation.ViewModels;
using WebApi.Filters;

namespace Web_App_Mvc_Presentation.Controllers;

public class AdminController(DataContext context, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly DataContext _context = context;

    [Route("/admin")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var contacts = await _context.Contact.ToListAsync();
        return View(contacts);
    }

    [HttpGet]
    public IActionResult CreateCourse()
    {
        return View();
    }

    
    
    [HttpPost]
    public async Task< IActionResult> CreateCourse(CourseModel model)
    {
        
        if (ModelState.IsValid)
        {
            if (!await _context.Courses.AnyAsync(x => x.Title == model.Title))
            {
                var corseEntity = new CourseEntity
                {
                    Title = model.Title,
                    Author = model.Author,
                    IsBestSeller = model.IsBestSeller,
                    DiscountPrice = model.DiscountPrice,
                    OrginalPrice = model.OrginalPrice,
                    LikesInNumber = model.LikesInNumber,
                    LikesInProcent = model.LikesInProcent,
                    Hours = model.Hours,
                    ImageName = model.ImageName,
                    Category = model.Category,
                    CreatedAt = DateTime.Now
                };
                _context.Courses.Add(corseEntity);
                await _context.SaveChangesAsync();
                ViewData["SuccessMessage"] = "Course was Created";
                return View();
            }
            else
            {
                ViewData["ErrorMessage"] = "Course already exist";
                return View();
            }
        }
        ViewData["ErrorMessage"] = "You must register";
        return View();
    }
}
      
