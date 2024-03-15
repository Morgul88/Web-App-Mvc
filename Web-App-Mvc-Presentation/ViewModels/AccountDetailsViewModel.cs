using Infrastructure.Models;
using Web_App_Mvc_Presentation.Models;

namespace Web_App_Mvc_Presentation.ViewModels;

public class AccountDetailsViewModel
{
    public ApplicationUser User { get; set; } = null!;

    public AdressDetailViewModel AdressInfo { get; set; } = null!;

    public BasicInfoDetailViewModel BasicInfo { get; set; } = null!;

    public ProfileInfoViewModel ProfileInfo { get; set; } = null!;
}
