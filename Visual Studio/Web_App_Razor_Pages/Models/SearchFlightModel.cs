using System.ComponentModel.DataAnnotations;

namespace Web_App_Razor_Pages.Models
{
	public class SearchFlightModel
	{
		[Required(ErrorMessage = "Origin is required.")]
		[StringLength(100, ErrorMessage = "Origin cannot be longer than 100 characters.")]
		public string Origin { get; set; }

		[Required(ErrorMessage = "Destination is required.")]
		[StringLength(100, ErrorMessage = "Destination cannot be longer than 100 characters.")]
		public string Destination { get; set; }

		[Required(ErrorMessage = "Departure date is required.")]
		[DataType(DataType.Date)]
		public DateTime DepartureDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ReturnDate { get; set; }
	}
}
