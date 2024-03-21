using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controller;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create(CourseDto dto)
    {
        if(ModelState.IsValid)
        {
            if(!await _context.Courses.AnyAsync(x => x.Title == dto.Title))
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

                };
                _context.Courses.Add(corseEntity);
                await _context.SaveChangesAsync();

                return Created("",null);
            }
            return Conflict();
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _context.Courses.ToListAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var courseEntity = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if(courseEntity != null)
        {
            return Ok(courseEntity);
        }
        else
        {
            return NotFound();
        }
    }
}
