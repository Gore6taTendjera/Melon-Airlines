using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using DTOs;
using Enums;
using Logic_Layer.Interface.LL;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;


namespace Web_App_Razor_Pages.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserLoginDTO Login { get; set; }

        private readonly IUserAccountService _userAccountService;
        private readonly IPassportService _passportService;
        private readonly IIDService _idService;


        public LoginModel(IUserAccountService userAccountService, IPassportService passportService, IIDService iDService)
        {
            _userAccountService = userAccountService;
            _passportService = passportService;
            _idService = iDService;
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (ModelState.IsValid)
            {
                UserLoginDTO authenticatedUser = _userAccountService.Authenticate(Login.Username, Login.Password);
                if (authenticatedUser != null)
                {
                    HttpContext.Session.SetString(nameof(Login.Username), authenticatedUser.Username);
                    HttpContext.Session.SetString(nameof(Login.Password), authenticatedUser.Password);
                    HttpContext.Session.SetString(nameof(Login.ID), authenticatedUser.ID.ToString());

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Username),
                        new Claim(ClaimTypes.Name, authenticatedUser.Username),
                        new Claim(ClaimTypes.Role, authenticatedUser.UserType.ToString()),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    SetHasDocuments();

                    if (Login.RememberMe)
                    {
                        CookieOptions options = new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddSeconds(30)
                        };
                        Response.Cookies.Append(nameof(Login.Username), Login.Username, options);
                    }
                    else
                    {
                        Response.Cookies.Delete("Username");
                    }


                    if (authenticatedUser.UserType == UserType.Admin)
                    {
                        return RedirectToPage("/Dashboard");
                    }
                    else if (authenticatedUser.UserType == UserType.Normal)
                    {
                        return RedirectToPage("/Profile/Profile");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(Login), "Username or password is incorrect");
                    return Page();
                }
            }
            return Page();
        }


		public bool HasPassport()
		{
			int userId = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			return _passportService.HasPassport(userId);
		}

		public bool HasID()
		{
			int userId = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			return _idService.HasID(userId);
		}

		private void SetHasDocuments()
		{
            bool pass = HasPassport();
            bool id = HasID();
			if (pass && id)
			{
				HttpContext.Session.SetString("HasDocuments", true.ToString());
			}
			else
			{
				HttpContext.Session.SetString("HasDocuments", false.ToString());
			}
		}

	}
}