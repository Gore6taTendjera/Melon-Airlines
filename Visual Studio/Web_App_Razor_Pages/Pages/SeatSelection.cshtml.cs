using DTOs;
using Enums;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services.Planes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_App_Razor_Pages.Models;

namespace Web_App_Razor_Pages.Pages
{
    public class SeatSelectionModel : PageModel
    {
        private readonly IFlightService _flightService;
        private readonly IPlaneService _planeService;
		private readonly ISeatService _seatService;

        public SeatSelectionModel(IFlightService flightService, IPlaneService planeService, ISeatService seatService)
        {
            _flightService = flightService;
            _planeService = planeService;
			_seatService = seatService;
        }

        [BindProperty]
        public PlaneDTO plane { get; set; }

        [BindProperty]
        public FlightDTO flight { get; set; }

        [BindProperty]
        public SeatModelWeb seatModel { get; set; }


        public List<Seat> Seats { get; set; }

        public int MaxRow { get; set; }


        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(nameof(UserType.Normal)) && HttpContext.Session.Keys.Any())
            {
				bool hasDocs = Convert.ToBoolean(HttpContext.Session.GetString("HasDocuments"));
				if (hasDocs) 
				{
					int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));
					if (flightid <= 0)
					{
						return RedirectToPage("/Book");
					}

					plane = _planeService.GetPlaneByFlightIDDTO(flightid);
					flight = _flightService.GetFlightByIdDTO(flightid);
					Seats = _seatService.GetAllSeatsByFlightPlaneDTO(plane);

					MaxRow = Seats.Max(s => s.Row);

					seatModel = new SeatModelWeb
					{
						SeatRow = 0,
						SeatColumn = ' ',
						SeatPrice = 0
					};
					return Page();
				}
				else
				{
					return RedirectToPage("/profile/documents");
				}
            }
            else
            {
                return RedirectToPage("/Book");
            }
        }

		public IActionResult OnPostConfirm()
		{
			string columnString = HttpContext.Session.GetString(nameof(SeatModelWeb.SeatColumn));
			string rowString = HttpContext.Session.GetString(nameof(SeatModelWeb.SeatRow));
			string priceString = HttpContext.Session.GetString(nameof(SeatModelWeb.SeatPrice));

			if (string.IsNullOrEmpty(columnString) || string.IsNullOrEmpty(rowString) || string.IsNullOrEmpty(priceString))
			{
				LoadPlaneSeats();
				return Page();
			}

			return RedirectToPage("/payment");
		}



		public IActionResult OnPostCalculatePrice()
		{
			string seatSelection = Request.Form["seatSelection"];
			string seatClass = Request.Form["seatClass"];

			int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));

			if (seatSelection == "random" && Enum.TryParse<SeatModel>(seatClass, out SeatModel selectedSeatClass))
			{
				Seat nextAvailableSeat = GetNextAvailableSeat(selectedSeatClass);
				if (nextAvailableSeat != null)
				{
					seatModel.SeatRow = nextAvailableSeat.Row;
					seatModel.SeatColumn = nextAvailableSeat.Column;
					seatModel.seatModel = nextAvailableSeat.SeatModel;
					seatModel.SeatPrice = _flightService.GetFlightPrice(flightid, nextAvailableSeat.SeatModel);

					HttpContext.Session.SetString(nameof(seatModel.SeatRow), seatModel.SeatRow.ToString());
					HttpContext.Session.SetString(nameof(seatModel.SeatColumn), seatModel.SeatColumn.ToString());
					HttpContext.Session.SetString(nameof(seatModel.SeatPrice), seatModel.SeatPrice.ToString());
					HttpContext.Session.SetString(nameof(seatModel.seatModel), seatModel.seatModel.ToString());
				}

				LoadPlaneSeats();
				return Page();
			}
			else if (seatSelection == "selectNow" && seatModel.SeatRow > 0 && seatModel.SeatColumn != default(char))
			{
				LoadPlaneSeats();
				var selectedSeat = Seats.FirstOrDefault(s => s.Row == seatModel.SeatRow && s.Column == seatModel.SeatColumn);
				if (selectedSeat != null)
				{
					seatModel.seatModel = selectedSeat.SeatModel;
					seatModel.SeatPrice = _flightService.GetFlightPrice(flightid, selectedSeat.SeatModel);

					HttpContext.Session.SetString(nameof(seatModel.SeatRow), seatModel.SeatRow.ToString());
					HttpContext.Session.SetString(nameof(seatModel.SeatColumn), seatModel.SeatColumn.ToString());
					HttpContext.Session.SetString(nameof(seatModel.SeatPrice), seatModel.SeatPrice.ToString());
					HttpContext.Session.SetString(nameof(seatModel.seatModel), seatModel.seatModel.ToString());

				}

				return Page();
			}
			return Page();
		}


		private void LoadPlaneSeats()
		{
			int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));
			plane = _planeService.GetPlaneByFlightIDDTO(flightid);
			Seats = _seatService.GetAllSeatsByFlightPlaneDTO(plane);
			MaxRow = Seats.Max(s => s.Row);
		}

		private Seat GetNextAvailableSeat(SeatModel seatModel)
		{
			int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));

			switch (seatModel)
			{
				case SeatModel.First:
					return _seatService.GetAvailableFirstSeatByFlightID(flightid);

				case SeatModel.Business:
					return _seatService.GetAvailableBusinessSeatByFlightID(flightid);

				case SeatModel.Economy:
					return _seatService.GetAvailableEconomySeatByFlightID(flightid);

				default:
					throw new ArgumentException("Invalid seat model");
			}
		}




		public double GetPriceForFlightSeatModel(FlightDTO flight, SeatModel seatModel)
		{
			return _flightService.GetFlightPrice(flight.FlightId, seatModel);
		}


		public void ToPayment()
		{
			HttpContext.Session.SetString(nameof(seatModel.SeatRow), seatModel.SeatRow.ToString());
			HttpContext.Session.SetString(nameof(seatModel.SeatColumn), seatModel.SeatColumn.ToString());
			HttpContext.Session.SetString(nameof(seatModel.SeatPrice), seatModel.SeatPrice.ToString());
			HttpContext.Session.SetString(nameof(seatModel.seatModel), seatModel.seatModel.ToString());


		}

	}
}
