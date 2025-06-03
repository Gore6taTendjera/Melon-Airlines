using DTOs;
using Enums;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared_Classes;
using System.Numerics;
using Web_App_Razor_Pages.Models;

namespace Web_App_Razor_Pages.Pages
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public FlightDTO flight { get; set; }
        [BindProperty]
        public SeatModelWeb seatModel { get; set; }

        [BindProperty]
        public CreditCardDTO creditCard { get; set; }

        [BindProperty]
        public PayPalDTO payPal { get; set; }

        private readonly IFlightService _flightService;
        private readonly ITicketService _ticketService;
        private readonly IPaymentService _paymentService;

        public PaymentModel(IFlightService flightService, ITicketService ticketService, IPaymentService paymentService)
        {
            _flightService = flightService;
            _ticketService = ticketService;
            _paymentService = paymentService;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(nameof(UserType.Normal)) && HttpContext.Session.Keys.Any())
            {
                int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));
                if (flightid <= 0)
                {
                    return RedirectToPage("/index");
                }

                flight = _flightService.GetFlightByIdDTO(flightid);
                this.seatModel = new SeatModelWeb()
                {
                    SeatColumn = Convert.ToChar(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatColumn))),
                    SeatRow = Convert.ToInt32(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatRow))),
                    SeatPrice = Convert.ToDouble(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatPrice))),
					seatModel = (SeatModel)Enum.Parse(typeof(SeatModel), HttpContext.Session.GetString(nameof(SeatModelWeb.seatModel)))
				};

                return Page();
            }
            else
            {
                return RedirectToPage("/index");
            }
        }


        private void LoadData()
        {

        }

        public IActionResult OnPostCreditCardPayment()
        {
            if (!ModelState.IsValid)
            {
                if (_paymentService.ProcessCreditCardPayment(creditCard)) 
                {
                    if (CreateTicket())
                    {
                        return RedirectToPage("/index");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(creditCard), "Credit Card is invalid");
                    int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));
                    if (flightid <= 0)
                    {
                        return RedirectToPage("/index");
                    }

                    flight = _flightService.GetFlightByIdDTO(flightid);
                    this.seatModel = new SeatModelWeb()
                    {
                        SeatColumn = Convert.ToChar(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatColumn))),
                        SeatRow = Convert.ToInt32(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatRow))),
                        SeatPrice = Convert.ToDouble(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatPrice))),
                        seatModel = (SeatModel)Enum.Parse(typeof(SeatModel), HttpContext.Session.GetString(nameof(SeatModelWeb.seatModel)))
                    };
                }
                return Page();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPostPayPalPayment()
        {
            if (!ModelState.IsValid)
            {
                if (_paymentService.ProcessPayPalPayment(payPal))
                {
                    if (CreateTicket())
                    {
                        return RedirectToPage("/index");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(payPal), "PayPal Email is invalid");
                    int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));
                    if (flightid <= 0)
                    {
                        return RedirectToPage("/index");
                    }

                    flight = _flightService.GetFlightByIdDTO(flightid);
                    this.seatModel = new SeatModelWeb()
                    {
                        SeatColumn = Convert.ToChar(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatColumn))),
                        SeatRow = Convert.ToInt32(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatRow))),
                        SeatPrice = Convert.ToDouble(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatPrice))),
                        seatModel = (SeatModel)Enum.Parse(typeof(SeatModel), HttpContext.Session.GetString(nameof(SeatModelWeb.seatModel)))
                    };
                }
                return Page();
            }
            else
            {
                return Page();
            }
        }

        public bool CreateTicket()
        {
            int flightid = Convert.ToInt32(HttpContext.Session.GetString(nameof(Flight.FlightID)));
            int userId = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
            char column = Convert.ToChar(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatColumn)));
            int row = Convert.ToInt32(HttpContext.Session.GetString(nameof(SeatModelWeb.SeatRow)));
            SeatModel model = (SeatModel)Enum.Parse(typeof(SeatModel), HttpContext.Session.GetString(nameof(SeatModelWeb.seatModel)));

            return _ticketService.CreateTicket(flightid, userId, model, row, column);
        }

    }
}
