using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
	public class FlightDTO
	{
		public int FlightId {  get; set; }
		public Airport DepartureAirport { get; set; }
		public DateTime DepartureTime { get; set; }
		public Airport ArrivalAirport { get; set; }
		public DateTime ArrivalTime { get; set;}
		public Plane Plane { get; set; }
		public FlightStatus FlightStatus { get; set; }
		public double Price { get; set; }

	}
}
