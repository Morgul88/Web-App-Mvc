using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web_App_Mvc_Presentation.ViewModels;

public class ProfileInfoViewModel
{

    public string ProfileImage { get; set; } = "/avatar.svg";

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAdress { get; set; } = null!;
}
