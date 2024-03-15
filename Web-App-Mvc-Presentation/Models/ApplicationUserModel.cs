using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.Models;

public class ApplicationUserModel
{
    [Required]
    [Display(Name = "First name")]
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    [ProtectedPersonalData]
    public string LastName { get; set;} = null!;
}
