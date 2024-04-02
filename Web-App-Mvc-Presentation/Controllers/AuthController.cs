using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Web_App_Mvc_Presentation.Models;
using Web_App_Mvc_Presentation.ViewModels;

namespace Web_App_Mvc_Presentation.Controllers;


public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AdressService adressService, HttpClient client, IConfiguration configuration) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly AdressService _adressService = adressService;
    private readonly HttpClient _client = client;
    private readonly IConfiguration _configuration = configuration;

    #region Register/CreateUser
    [HttpGet]
    [Route("/register")]
    public IActionResult Register()
    {

        return View();
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            if (!await _userManager.Users.AnyAsync(x => x.Email == model.EmailAdress))
            {
                var applicationUser = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.EmailAdress,
                    UserName = model.EmailAdress
                };

                var registerResult = await _userManager.CreateAsync(applicationUser, model.Password);
                if (registerResult.Succeeded)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(applicationUser, model.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Email adress already exist";
                return View(model);
            }
        }
        ViewData["ErrorMessage"] = "You must register";
        return View(model);
    }
    #endregion

    #region LoginIn
    [HttpGet]
    [Route("/login")]
    public IActionResult Login()
    {

        return View();
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> Login(SignInModel form)
    {
        if (ModelState.IsValid)
        {


            var signInResult = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
            if (signInResult.Succeeded)
            {
                
                var content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json" );

                var response = await _client.PostAsync($"https://localhost:7070/api/Auth/token?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    var cookieOptions = new CookieOptions()
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddDays(1)
                    };

                    Response.Cookies.Append("AccessToken", token, cookieOptions);
                }

                

                return RedirectToAction("Index", "Home");
            }
        }

        ViewData["ErrorMessage"] = "Invalid email or password";
        return View(form);
    }



    #endregion

    #region Logout
    [HttpGet]
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData["SuccessMessage"] = "You have logged out";
        return RedirectToAction("Index", "Home");

    }
    #endregion

    #region Details
    [Authorize]
    [HttpGet]
    [Route("/auth/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel();

        viewModel.BasicInfo = await PopulateBasicInfoAsync();
        viewModel.AdressInfo ??= await PopulateAdressInfoAsync();
        viewModel.ProfileInfo ??= await PopulateProfileInfoAsync();
        return View(viewModel);

    }

    [HttpPost]
    [Route("/auth/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        if (viewModel.BasicInfo != null)
        {

            if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.EmailAdress != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfo.FirstName;
                    user.LastName = viewModel.BasicInfo.LastName;
                    user.Email = viewModel.BasicInfo.EmailAdress;
                    user.PhoneNumber = viewModel.BasicInfo.Phone;
                    user.Bio = viewModel.BasicInfo.Biography;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong! unable to save data.");
                        ViewData["ErrorMessage"] = "Something went wrong! Unable to save data.";
                    }
                    if (result.Succeeded)
                    {
                        ViewData["SuccessMessage"] = "Data saved successfully";
                    }
                }
            }

        }
        if (viewModel.AdressInfo != null)
        {

            if (viewModel.AdressInfo.AdressLine_1 != null && viewModel.AdressInfo.PostalCode != null && viewModel.AdressInfo.City != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var adress = await _adressService.GetAddressAsync(user.Id);
                    if (adress != null)
                    {
                        adress.StreetName = viewModel.AdressInfo.AdressLine_1;
                        adress.AddressLine_2 = viewModel.AdressInfo.AdressLine_2;
                        adress.PostalCode = viewModel.AdressInfo.PostalCode;
                        adress.City = viewModel.AdressInfo.City;

                        var result = await _adressService.UpdateAdressAsync(adress);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong! unable to save data.");
                            ViewData["ErrorMessage"] = "Something went wrong! Unable to save data.";
                        }
                        else
                        {
                            ViewData["SuccessMessage"] = "Data saved successfully";
                        }

                    }
                    else
                    {
                        adress = new AddressEntities
                        {
                            ApplicationUserId = user.Id,
                            StreetName = viewModel.AdressInfo.AdressLine_1,
                            AddressLine_2 = viewModel.AdressInfo.AdressLine_2,
                            City = viewModel.AdressInfo.City,
                            PostalCode = viewModel.AdressInfo.PostalCode
                        };
                        var result = await _adressService.CreateAdressAsync(adress);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong! unable to save data.");
                            ViewData["ErrorMessage"] = "Something went wrong! Unable to save data.";
                        }
                        else
                        {
                            ViewData["SuccessMessage"] = "Data saved successfully";
                        }
                    }



                }
            }

        }

        viewModel.BasicInfo = await PopulateBasicInfoAsync();
        viewModel.AdressInfo ??= await PopulateAdressInfoAsync();
        viewModel.ProfileInfo ??= await PopulateProfileInfoAsync();

        return View(viewModel);
    }
    //[HttpPost]
    //public IActionResult SaveBasicInfo(AccountDetailsViewModel viewModel)
    //{
    //    if (TryValidateModel(viewModel.BasicInfo))
    //    {
    //        return RedirectToAction("Home", "Index");
    //    }
    //    return RedirectToAction("Details",viewModel);
    //}

    //[HttpPost]
    //public IActionResult SaveAddressInfo(AccountDetailsViewModel viewModel)
    //{
    //    if (TryValidateModel(viewModel.AdressInfo))
    //    {
    //        return RedirectToAction("Home", "Index");
    //    }
    //    return View("Details", viewModel);
    //}



    public async Task<BasicInfoDetailViewModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new BasicInfoDetailViewModel
        {
            UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAdress = user.Email!,
            Phone = user.PhoneNumber,
            Biography = user.Bio,
            IsExternalAccount = user.IsExternalAccount,
        };
    }
    public async Task<AdressDetailViewModel> PopulateAdressInfoAsync()
    {

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var adress = await _adressService.GetAddressAsync(user.Id);
            if (adress != null)
            {
                return new AdressDetailViewModel
                {
                    AdressLine_1 = adress.StreetName!,
                    AdressLine_2 = adress.AddressLine_2,
                    PostalCode = adress.PostalCode!,
                    City = adress.City!,
                };
            }

        }

        return new AdressDetailViewModel();
    }

    public async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel
        {

            FirstName = user!.FirstName,
            LastName = user.LastName,
            EmailAdress = user.Email!,


        };
    }
    #endregion

    #region BasicInfo
    [HttpPost]
    public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
    {
        var result = await _userManager.UpdateAsync(viewModel.User);
        if (result.Succeeded)
        {
            return RedirectToAction("Details", "Auth", viewModel);
        }
        else
        {
            return View(viewModel);
        }
    }
    #endregion

    #region Security
    [HttpGet]
    [Route("/security")]
    public async Task<IActionResult> Security()
    {
        if (!_signInManager.IsSignedIn(User))
            return RedirectToAction("Index", "Home");

        var userEntity = await _userManager.GetUserAsync(User);

        var viewModel = new AccountDetailsViewModel()
        {
            User = userEntity!
        };
        viewModel.ProfileInfo ??= await PopulateProfileInfoAsync();
        return View(viewModel);

    }
    [HttpPost]
    [Route("/security")]
    public async Task<IActionResult> Security(AccountDetailsViewModel viewModel)
    {
        if (viewModel.SecurityInfo != null)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, viewModel.SecurityInfo.Password);

                if (result)
                {
                    var changePassword = await _userManager.ChangePasswordAsync(user, viewModel.SecurityInfo.Password, viewModel.SecurityInfo.NewPassword);


                    if (changePassword.Succeeded)
                    {
                        var updatedResult = await _userManager.UpdateAsync(user);
                        if (updatedResult.Succeeded)
                        {
                            ViewData["SuccessMessage"] = "Data saved successfully";
                            return View(viewModel);
                        }
                        else
                        {

                            ModelState.AddModelError(string.Empty, "Something went wrong.");
                            return View(viewModel);
                        }

                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Password didnt match.");
                    return View(viewModel);
                }
            }

        }



        if (viewModel.DeleteAccount?.DeleteAccount ?? false)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var deleteResult = await _userManager.DeleteAsync(user!);

                if (deleteResult.Succeeded)
                {
                    TempData["SuccessMessage"] = "Account was removed, you will be logged out soon...";

                    await Task.Delay(5000);
                    return RedirectToAction("Logout", "Auth");

                }

                ViewData["ErrorMessage"] = "Something went wrong";
                return View(viewModel);
            }

        }

        ViewData["ErrorMessage"] = "You have to check the box to delete..";
        return View(viewModel);
    }


    #endregion

    #region Facebook

    [HttpGet]
    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallBack"));

        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info != null)
        {
            var userEntity = new ApplicationUser
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true,
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                    user = await _userManager.FindByEmailAsync(userEntity.Email);

            }

            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;


                    await _userManager.UpdateAsync(user);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                {
                    return RedirectToAction("Details", "Auth");
                }
            }
            ViewData["StatusMessage"] = "Failed to login with facebook account";
            return RedirectToAction("Login", "Auth");

        }

        return RedirectToAction("Index", "Home");
    }

    #endregion
}
