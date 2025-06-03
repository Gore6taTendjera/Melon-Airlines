using Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Web_App_Razor_Pages.Pages
{
    public class DashboardModel : PageModel
    {

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(nameof(UserType.Admin)))
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        public IActionResult OnPost()
        {

            HttpContext.SignOutAsync();
            return RedirectToPage("/Login");

        }
    }
}
