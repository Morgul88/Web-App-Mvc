using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (!await _context.Subscribers.AnyAsync(x => x.Email == email))
                {
                    try
                    {
                        var subscriberEntity = new SubscriberEntity { Email = email };
                        _context.Subscribers.Add(subscriberEntity);
                        await _context.SaveChangesAsync();

                        return Created("", null);
                    }
                    catch
                    {
                        return Problem("Unable to create subscription");
                    }

                }
                return Conflict("Your email has already been subscribed");

            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscribers = await _context.Subscribers.ToListAsync();
            if (subscribers.Count != 0)
            {
                return Ok(subscribers);
            }
            return NotFound();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

            if(subscriber != null)
            {
                return Ok(subscriber);  
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string email)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

            if(subscriber != null)
            {
                if(!await _context.Subscribers.AnyAsync(x => x.Email == email))
                {
                    subscriber!.Email = email;
                    _context.Subscribers.Update(subscriber);
                    await _context.SaveChangesAsync();
                    return Ok(subscriber);
                }
                return Conflict("There is someone with same email adress");
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

            if (subscriber != null)
            {
               
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
                return Ok();
                
            }
            return NotFound();
        }
    }
}
