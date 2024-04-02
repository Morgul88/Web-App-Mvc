using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.Models;

public class SignInModel
{
    [Required(ErrorMessage = "A Email is required")]
    [Display(Name = "Email", Prompt ="Your email adress")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A Password is required")]
    [Display(Name = "Password", Prompt = "Your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
