using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.Models;

public class ContactModel
{
    [Required(ErrorMessage = "A Full name is required")]
    [Display(Name = "Name", Prompt = "Your full name")]
    [MinLength(2, ErrorMessage = "Invalid First name")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Email adress", Prompt = "Enter you Email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
    [Required(ErrorMessage = "Invalid email")]
    public string EmailAdress { get; set; } = null!;

    [Required(ErrorMessage = "A service is required")]
    [Display(Name = "Service", Prompt = "Choose the service you are intrested in")]
    [MinLength(2, ErrorMessage = "Invalid Service")]
    public string Service { get; set; } = null!;

    [Required(ErrorMessage = "A Password is required")]
    [Display(Name = "Message", Prompt = "Enter your message here...")]
    public string Message { get; set; } = null!;

}
