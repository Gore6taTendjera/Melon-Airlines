using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shared_Classes;

namespace DTOs
{
	public class TicketDTO
	{
		public int FlightID { get; set; }
		public int? SeatRow { get; set; }
		public char? SeatColumn { get; set; }
		public SeatModel? SeatModel { get; set; }

		[ValidateNever]
		public int UserID { get; set; }
	}
}
