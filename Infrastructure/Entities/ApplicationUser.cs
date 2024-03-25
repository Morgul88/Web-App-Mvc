using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ApplicationUser: IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set;} = null!;

    [ProtectedPersonalData]
    public string? Bio { get; set; }

    public AddressEntities? Address { get; set; }

    public bool IsExternalAccount { get; set; } = false;
}
