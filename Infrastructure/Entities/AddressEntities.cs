using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AddressEntities
{ 
    public int Id { get; set; }
    public string? StreetName { get; set; } 
    public string? AddressLine_2 { get; set; }
    public string? PostalCode { get; set; } 
    public string? City { get; set; } 

    public string? ApplicationUserId { get; set; }
    public ICollection<ApplicationUser> Users { get; set; } = [];

    
}
