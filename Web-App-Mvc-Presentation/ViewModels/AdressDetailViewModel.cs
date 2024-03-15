using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.ViewModels
{
    public class AdressDetailViewModel
    {
        [Display(Name = "Adress Line 2", Prompt = "Enter your adress line", Order = 0)]
        [Required(ErrorMessage = "Adress is required")]
        public string AdressLine_1 { get; set; } = null!;

        [Display(Name = "Adress Line 1", Prompt = "Enter your adress line", Order = 1)]
        public string? AdressLine_2 { get; set; }

        [Display(Name = "Postal Code", Prompt = "Enter you Postal Code", Order = 2)]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = null!;
    }
}
