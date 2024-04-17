using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApi.Filters;

namespace WebApi.Controller;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(DataContext context, HttpClient httpClient) : ControllerBase
{
    private readonly DataContext _context = context;
    private readonly HttpClient _httpClient = httpClient;

    [Authorize]
    [UseApiKey]
    [HttpPost]
    public async Task<IActionResult> Create(CourseDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _context.Courses.AnyAsync(x => x.Title == dto.Title))
            {
                var corseEntity = new CourseEntity
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    IsBestSeller = dto.IsBestSeller,
                    DiscountPrice = dto.DiscountPrice,
                    OrginalPrice = dto.OrginalPrice,
                    LikesInNumber = dto.LikesInNumber,
                    LikesInProcent = dto.LikesInProcent,
                    Hours = dto.Hours,
                    ImageName = dto.ImageName,
                    Category = dto.Category,
                    CreatedAt = DateTime.Now
                };
                _context.Courses.Add(corseEntity);
                await _context.SaveChangesAsync();

                return Created("", null);
            }
            return Conflict();
        }

        return BadRequest();
    }


    [Authorize]
    [UseApiKey]
    [HttpGet]
    public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {

        var query = _context.Courses.AsQueryable();
        if (!string.IsNullOrEmpty(category) && category != "all")
        {
            query = query.Where(x => x.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.Title.Contains(searchQuery) || x.Author!.Contains(searchQuery));
        }
        query = query.OrderByDescending(o => o.Title);

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var courses = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var courseModel = CourseFactory.Create(courses);

        var response = new CourseResult
        {
            Succeeded = true,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Courses = courseModel,
        };

        return Ok(response);
        
    }

    [Authorize]
    [UseApiKey]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {


        var courseEntity = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (courseEntity != null)
        {
            return Ok(courseEntity);
        }
        else
        {
            return NotFound();
        }
    }

    [Authorize]
    [UseApiKey]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CourseDto dto)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (course != null)
        {

            course.Title = dto.Title;
            course.Author = dto.Author;
            course.IsBestSeller = dto.IsBestSeller;
            course.DiscountPrice = dto.DiscountPrice;
            course.OrginalPrice = dto.OrginalPrice;
            course.LikesInNumber = dto.LikesInNumber;
            course.LikesInProcent = dto.LikesInProcent;
            course.Hours = dto.Hours;
            course.ImageName = dto.ImageName;
            course.Category = dto.Category;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        return NotFound();
    }

    [Authorize]
    [UseApiKey]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (course != null)
        {

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok();

        }
        return NotFound();
    }
}
