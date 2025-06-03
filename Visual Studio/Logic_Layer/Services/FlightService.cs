using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Shared_Classes;
using Enums;
using DTOs;
using Logic_Layer.Services.Planes;

namespace Logic_Layer
{
    public class FlightService : IFlightService
    {
        private readonly IFlightDAL _flightDAL;

        public FlightService(IFlightDAL flightDAL)
        {
            _flightDAL = flightDAL;
        }


        // for admin
        public bool CreateFlight(Airport origin, Airport destination, DateTime takeoff, DateTime arrival, double price, Plane plane, FlightStatus flightStatus)
        {
            Flight flight = new(origin, destination, takeoff, arrival, price, plane, flightStatus);
            return _flightDAL.CreateFlight(flight);
        }


        public double GetFlightPrice(int flightId, SeatModel seatModel)
        {
            return _flightDAL.GetFlightPrice(flightId, seatModel);
        }

        public Flight GetFlightByID(int id)
        {
            return _flightDAL.GetFlightByID(id);
        }

        public FlightDTO GetFlightByIdDTO(int id)
        {
			Flight flight = _flightDAL.GetFlightByID(id);

            FlightDTO flightDTOs = new FlightDTO
            {
                FlightId = flight.FlightID,
                DepartureAirport = flight.DepartureAirport,
                DepartureTime = flight.DepartureTime,
                ArrivalAirport = flight.ArrivalAirport,
                ArrivalTime = flight.ArrivalTime,
                Plane = flight.Plane,
                FlightStatus = flight.FlightStatus,
                Price = flight.Price
            };
            return flightDTOs;
		}

        public List<Flight> GetAllFlights()
        {
            return _flightDAL.GetAllFlights();
        }

		public List<FlightDTO> GetAllFlightsByLocationTimeDateDTO(string originCity, string destinationCity, DateTime departureDate, DateTime? returnDate)
		{
			List<Flight> flights;
			if (!returnDate.HasValue)
			{
				flights = GetAllFlightsByLocationTimeDate(originCity, destinationCity, departureDate);
			}
			else
			{
				flights = GetAllFlightsByLocationTimeDate(originCity, destinationCity, departureDate, returnDate.Value);
			}

			List<FlightDTO> flightDTOs = new List<FlightDTO>();

			foreach (var flight in flights)
			{
				FlightDTO flightDTO = new FlightDTO
				{
					FlightId = flight.FlightID,
					DepartureAirport = flight.DepartureAirport,
					DepartureTime = flight.DepartureTime,
					ArrivalAirport = flight.ArrivalAirport,
					ArrivalTime = flight.ArrivalTime,
					Plane = flight.Plane,
					FlightStatus = flight.FlightStatus,
					Price = flight.Price
				};
				flightDTOs.Add(flightDTO);
			}

			return flightDTOs;
		}


		public List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate, DateTime returnDate)
        {
            return _flightDAL.GetAllFlightsByLocationTimeDate(originCity, destinationCity, departureDate, returnDate);
        }

		public List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate)
		{
			return _flightDAL.GetAllFlightsByLocationTimeDate(originCity, destinationCity, departureDate);
		}



		public bool UpdateFlight(Flight flight)
        {
            return _flightDAL.UpdateFlight(flight);
        }



        public bool DeleteFlightByID(int id)
        {
            return _flightDAL.DeleteFlightByID(id);
        }

      
    }
}
