using Enums;
using Logic_Layer.Interface.LL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DTOs;

namespace Web_App_Razor_Pages.Pages.Profile
{
    public class TicketsPageModel : PageModel
    {
		private readonly ITicketService _ticketService;
        private readonly IFlightService _flightService;

        public List<TicketDTO> Tickets { get; set; }

		public TicketsPageModel(ITicketService ticketService, IFlightService flightService)
        {
            this._ticketService = ticketService;
            this._flightService = flightService;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(nameof(UserType.Normal)))
            {
                int userId = Convert.ToInt32(HttpContext.Session.GetString("ID"));
                Tickets = _ticketService.GetAllTicketsByPassengerIdDTO(userId);
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

  
        public FlightDTO GetFlightDTOByTicketId(int id)
        {
            return _flightService.GetFlightByIdDTO(id);
        }
    }
}
