using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using DTOs;
using Logic_Layer.Interface.LL;
using Enums;

namespace Web_App_Razor_Pages.Pages.Profile
{
    public class ProfilePageModel : PageModel
    {
        private readonly IUserAccountService _userAccountService;

        [BindProperty]
        public UserProfileDetailsDTO UserDetailsDTO { get; set; }

        public ProfilePageModel(IUserAccountService userAccountService) 
        {
            _userAccountService = userAccountService;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(nameof(UserType.Normal)))
            {
                if (!HttpContext.Session.Keys.Any())
                {
                    return LogoutUser();
                }

                //string username = HttpContext.Session.GetString("Username");
                //string password = HttpContext.Session.GetString("Password");
                int id = Convert.ToInt32(HttpContext.Session.GetString("ID"));

                UserDetailsDTO = _userAccountService.GetUserProfileDetails(id);

                return Page();
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        private IActionResult LogoutUser()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }

        public IActionResult OnPostLogout()
        {
            LogoutUser();
            return RedirectToPage("/Login");
        }
    }
}