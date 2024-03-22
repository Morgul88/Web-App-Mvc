using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.Models;

public class SubscribeModel
{
    [Display(Name = "Subscription", Prompt ="Enter your email adress")]
    [Required]
    [EmailAddress]
    public string? Email { get; set; } = null!;
}
