using Humanizer;
using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Web_App_Mvc_Presentation.Models;
using Web_App_Mvc_Presentation.ViewModels;

namespace Web_App_Mvc_Presentation.Controllers;



public class HomeController(DataContext context) : Controller
{
    private readonly DataContext _context = context;

    public IActionResult Index()
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
        var contactModel = new ContactModel();
        return View(contactModel);

        

    }

    [HttpPost]
    [Route("/contacts")]
    public async Task<IActionResult> Contacts(ContactModel contact)
    {
        if (ModelState.IsValid)
        {
            
            var contactEntity = new ContactEntity
            {
                FullName = contact.FullName,
                EmailAdress = contact.EmailAdress,
                Message = contact.Message,
                Service = contact.Service,
                CreatedAt = DateTime.Now,
            };

            _context.Contact.Add(contactEntity);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                    
                ViewData["SuccessMessage"] = "Your message was sent!";
                return View(); 
            }
            else
            {
                    
                ViewData["ErrorMessage"] = "Failed to send message. Please try again.";
            }
            
            
        }
        return View();
    }




}
