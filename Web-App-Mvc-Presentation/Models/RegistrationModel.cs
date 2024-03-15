using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.Models;

public class RegistrationModel
{
    [Display(Name = "First Name", Prompt = "Enter you First name", Order = 0)]
    [Required(ErrorMessage = "Invalid First name")]
    [MinLength(2, ErrorMessage = "Invalid First name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Enter you last name", Order = 1)]
    [Required(ErrorMessage = "Invalid Last name")]
    [MinLength(2, ErrorMessage = "Invalid Last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email adress", Prompt = "Enter you Email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
    [Required(ErrorMessage = "Invalid email")]
    public string EmailAdress { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter you password", Order = 3)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid Password")]
    [Required(ErrorMessage = "Invalid Password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password doesnt match")]
    [Required(ErrorMessage = "Invalid Password")]
    public string ConfirmPasword { get; set; } = null!;


    [Display(Name = "I agree in terms", Order = 5)]
    [CheckBoxRequired(ErrorMessage = "You must accept the terms and condition to proceed.")]
    public bool TermsAndConditions { get; set; } = false;
}
