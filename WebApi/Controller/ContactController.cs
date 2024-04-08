using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop.Infrastructure;
using WebApi.Filters;

namespace WebApi.Controller;

[Route("api/[controller]")]
[ApiController]
public class ContactController(DataContext dataContext, IConfiguration configuration) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;
    private readonly IConfiguration _configuration = configuration;

    
    [UseApiKey]
    [HttpPost]
    public async Task<IActionResult> Create(ContactDto dto)
    {
        if (ModelState.IsValid)
        {
            if (!await _dataContext.Contact.AnyAsync(x => x.EmailAdress == dto.EmailAdress))
            {
                var contactEntity = new ContactEntity
                {
                    FullName = dto.FullName,
                    EmailAdress = dto.EmailAdress,
                    Message = dto.Message,
                    Service = dto.Service,
                    CreatedAt = DateTime.Now

                };

                _dataContext.Contact.Add(contactEntity);
                await _dataContext.SaveChangesAsync();

                return Created("", null);
            }
            return Conflict();
        }

        return BadRequest();
    }

    
}
