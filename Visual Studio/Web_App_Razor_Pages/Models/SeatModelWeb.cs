using Enums;

namespace Web_App_Razor_Pages.Models
{
	public class SeatModelWeb
	{
		public int SeatRow { get; set; }
		public char SeatColumn { get; set;}
		public SeatModel seatModel { get; set; }
		public double SeatPrice { get; set; }
	}
}
