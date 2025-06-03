using DTOs;
using Enums;
using Logic_Layer.Interface.LL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared_Classes;
using Web_App_Razor_Pages.Models;
using Microsoft.AspNetCore.Http;

namespace Web_App_Razor_Pages.Pages
{
	public class BookModel : PageModel
	{
		[BindProperty]
		public SearchFlightModel FlightSearch { get; set; }
		public string ErrorMessage { get; set; }
		public string NoFlightsMessage { get; set; }

		[BindProperty]
		public string FlightType { get; set; }

		private readonly IFlightService _flightService;

		public BookModel(IFlightService flightService)
		{
			this._flightService = flightService;
		}

		public List<FlightDTO> Flights { get; set; }

		public IActionResult OnPostSearchFlights()
		{
			if (ModelState.IsValid)
			{
				DateTime? returnDate = FlightType == "two_way" ? FlightSearch.ReturnDate : null;
				Flights = _flightService.GetAllFlightsByLocationTimeDateDTO(
					FlightSearch.Origin,
					FlightSearch.Destination,
					FlightSearch.DepartureDate,
					returnDate
				);

				if (Flights == null || !Flights.Any())
				{
					NoFlightsMessage = "No flights found";
				}

				return Page();
			}

			return Page();
		}

        public IActionResult OnPostSelectFlight(int selectedFlightId)
        {
            if (!User.Identity.IsAuthenticated && !HttpContext.Session.Keys.Any())
            {
                ErrorMessage = "You must have an account to book a flight.";
                return Page();
            }
            else if (User.IsInRole(nameof(UserType.Normal)))
            {
				HttpContext.Session.SetString(nameof(Flight.FlightID), selectedFlightId.ToString());

				return RedirectToPage("/SeatSelection");
            }
            return Page();
        }

    }
}
