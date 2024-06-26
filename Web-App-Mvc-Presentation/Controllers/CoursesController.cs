﻿using Humanizer;
using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Migrations;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web_App_Mvc_Presentation.Models;
using Web_App_Mvc_Presentation.ViewModels;

namespace Web_App_Mvc_Presentation.Controllers;


public class CoursesController(HttpClient httpClient, IConfiguration configuration, DataContext context, CourseService courseService, CategoryService categoryService = null) : Controller
{

    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly DataContext _context = context;
    private readonly CourseService _courseService = courseService;
    private readonly CategoryService _categoryService = categoryService;

    [HttpGet]
    [Route("/Courses")]
    public async Task<IActionResult> CourseView(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {

            var courseResult = await _courseService.GetCoursesAsync(category,searchQuery, pageNumber, pageSize);
            if(courseResult != null)
            {
                var viewModel = new CourseIndexViewModel
                {
                    Categories = await _categoryService.GetAllCategoriesAsync(),

                    Courses = courseResult.Courses,
                    Pagination = new Pagination
                    {
                        PageSize = pageSize,
                        CurrentPage = pageNumber,
                        TotalPages = courseResult.TotalPages,
                        TotalItems = courseResult.TotalItems
                    }

                };
                return View(viewModel);
            }
            else
            {
                return View();
            }

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

    [HttpPost]
    [Route("/SavedItems")]
    public async Task<IActionResult> Join(string id)
    {

        if (HttpContext.Request.Cookies.TryGetValue("Accesstoken", out var token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _httpClient.GetAsync($"https://localhost:7070/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                //var courses = JsonConvert.DeserializeObject<CourseModel>(data);
                var entity = JsonConvert.DeserializeObject<SavedCoursesEntities>(data);

                var existingEntity = await _context.SavedCourses.FindAsync(id);

                if (existingEntity == null)
                {
                    entity!.CreatedAt = DateTime.UtcNow;
                    _context.SavedCourses.Add(entity!);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Kursen har sparats i dina sparade kurser.";
                }
                else
                {
                    TempData["ErrorMessage2"] = "Kursen finns redan sparad i dina kurser";
                    return RedirectToAction("CourseView", "Courses");
                }

                return RedirectToAction("CourseView", "Courses");



            }
            else
            {
                TempData["ErrorMessage"] = "Hittade inte kursen i databasen.";
                return RedirectToAction("CourseView", "Courses");
            }



        };

        return RedirectToAction("CourseView", "Courses");
    }

    [HttpGet]
    [Route("/SavedItems")]
    public async Task<IActionResult> Join()
    {
        var savedCourseFromDb = await _context.SavedCourses.ToListAsync();

        var viewModel = new SavedCoursesListViewModel
        {
            SavedCoursesList = savedCourseFromDb
        };

        return View(viewModel);
    }

    [HttpGet]
    [Route("/DeleteCourse")]
    public async Task<IActionResult> DeleteCourse(string id)
    {
        try
        {
            var course = await _context.SavedCourses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                _context.SavedCourses.Remove(course);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return RedirectToAction("Join", "Courses"); // Redirect till sidan där kurserna visas
                }
                else
                {
                    TempData["ErrorMessage"] = "Misslyckades med att ta bort kursen från databasen.";
                    return RedirectToAction("Join", "Courses");
                }
            }
        }
        catch (Exception ex)
        {
            // Logga undantaget eller hantera det på annat sätt
            TempData["ErrorMessage"] = "Ett fel uppstod vid försöket att ta bort kursen från databasen.";
            return RedirectToAction("Join", "Courses");
        }
    }





}


