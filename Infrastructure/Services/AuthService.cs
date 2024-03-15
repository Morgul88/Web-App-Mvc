using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuthService
{
    private readonly DataContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(DataContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    
}
