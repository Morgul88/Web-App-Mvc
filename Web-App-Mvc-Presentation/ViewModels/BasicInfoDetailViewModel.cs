using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.ViewModels
{
    public class BasicInfoDetailViewModel
    {
        public string UserId { get; set; } = null!;

        [Display(Name = "First Name", Prompt = "Enter you First name", Order = 0)]
        [Required(ErrorMessage = "Invalid First name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name", Prompt = "Enter you last name", Order = 1)]
        [Required(ErrorMessage = "Invalid Last name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email adress", Prompt = "Enter you Email", Order = 2)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
        [Required(ErrorMessage = "Invalid email")]
        public string EmailAdress { get; set; } = null!;

        [Display(Name = "Phone", Prompt = "Enter your Phone", Order = 3)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; } 

        [Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
        [DataType(DataType.MultilineText)]
        public string? Biography { get; set; }

        public bool IsExternalAccount { get; set; }
    }
}
