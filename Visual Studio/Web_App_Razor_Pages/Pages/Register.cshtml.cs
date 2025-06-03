using DTOs;
using Logic_Layer.Interface.LL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;

namespace Web_App_Razor_Pages.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserRegisterDTO Register { get; set; }

        private readonly IUserAccountService _userAccountService;

        public RegisterModel(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            if (ModelState.IsValid)
            {
                bool registrationSuccess = _userAccountService.Register(Register.Username, Register.Password, Register.Email);
                if (registrationSuccess)
                {
                    return RedirectToPage("/Login");
                }
                else
                {
                    ModelState.AddModelError("Username", "Username already exists. Please choose a different username.");
                }
            }
            return Page();
        }

    }
}
