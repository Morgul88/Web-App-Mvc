using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.ViewModels;

public class SecurityInfoViewModel
{
    [Required(ErrorMessage = "A Password is required")]
    [Display(Name = "Password", Prompt = "Your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "A Password is required")]
    [Display(Name = "New Password", Prompt = "New password")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;


    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Password doesnt match")]
    [Required(ErrorMessage = "Invalid Password")]
    public string ConfirmPasword { get; set; } = null!;


}
