using Logic_Layer.Interface.DAL;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace uMSTest_Unit_Testing
{
    public class FAKE_FlightsDAL : IFlightDAL
    {
        private Dictionary<int, Flight> _flights = new Dictionary<int, Flight>
        {
            {1, new Flight(1, new Airport(1, "AAA", "Dubai International", "Dubai", "UAE", "Dubai/UAE"), new Airport(2, "BBB", "Narrita Airport", "Tokyo", "Japan", "Tokyo/Japan"), DateTime.Now, DateTime.Now.AddHours(2), 1000.0, new A320(1, "REG123", PlaneModel.A320, 150, new Airport(1, "AAA", "Dubai International", "Dubai", "UAE", "Dubai/UAE"), PlaneStatus.InService), FlightStatus.LANDED)},
            {2, new Flight(2, new Airport(2, "BBB", "Narrita Airport", "Tokyo", "Japan", "Tokyo/Japan"), new Airport(1, "AAA", "Dubai International", "Dubai", "UAE", "Dubai/UAE"), DateTime.Now.AddDays(1), DateTime.Now.AddHours(2), 1000.0, new A380(1, "REG123", PlaneModel.A380, 150, new Airport(2, "BBB", "Narrita Airport", "Tokyo", "Japan", "Tokyo/Japan"), PlaneStatus.InService), FlightStatus.CANCELLED)}
        };

        public bool CreateFlight(Flight flight)
        {
            if (flight == null) return false;

            _flights.Add(flight.FlightID, flight);
            return true;
        }

        public List<Flight> GetAllFlights()
        {
            return new List<Flight>(_flights.Values);
        }

        public Flight GetFlightByID(int flightId)
        {
            return _flights.ContainsKey(flightId) ? _flights[flightId] : null;
        }

        public double GetFlightPrice(int flightID, SeatModel seatModel)
        {
            Flight flight = GetFlightByID(flightID);
            if (flight != null)
            {
                double basePrice = flight.Price;

                switch (seatModel)
                {
                    case SeatModel.First:
                        return basePrice * 1.30; // First class
                    case SeatModel.Business:
                        return basePrice * 1.14; // Business class
                    case SeatModel.Economy:
                        return basePrice; // Economy class
                }
            }
            return 0;
        }

        public bool UpdateFlight(Flight flight)
        {
            if (_flights.ContainsKey(flight.FlightID))
            {
                _flights[flight.FlightID] = flight;
                return true;
            }
            return false;
        }

        public bool DeleteFlightByID(int id)
        {
            if (_flights.ContainsKey(id))
            {
                _flights.Remove(id);
                return true;
            }
            return false;
        }

        public List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate, DateTime returnDate)
        {
            List<Flight> matchingFlights = new List<Flight>();

            foreach (var flight in _flights.Values)
            {
                if (flight.DepartureAirport.City.Equals(originCity, StringComparison.OrdinalIgnoreCase) &&
                    flight.ArrivalAirport.City.Equals(destinationCity, StringComparison.OrdinalIgnoreCase) &&
                    flight.DepartureTime.Date == departureDate.Date)
                {
                    matchingFlights.Add(flight);
                }
            }

            foreach (var flight in _flights.Values)
            {
                if (flight.DepartureAirport.City.Equals(destinationCity, StringComparison.OrdinalIgnoreCase) &&
                    flight.ArrivalAirport.City.Equals(originCity, StringComparison.OrdinalIgnoreCase) &&
                    flight.DepartureTime.Date == returnDate.Date)
                {
                    matchingFlights.Add(flight);
                }
            }

            return matchingFlights;
        }

        public List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate)
        {
            List<Flight> matchingFlights = new List<Flight>();

            foreach (var flight in _flights.Values)
            {
                if (flight.DepartureAirport.City.Equals(originCity, StringComparison.OrdinalIgnoreCase) &&
                    flight.ArrivalAirport.City.Equals(destinationCity, StringComparison.OrdinalIgnoreCase) &&
                    flight.DepartureTime.Date == departureDate.Date)
                {
                    matchingFlights.Add(flight);
                }
            }

            return matchingFlights;
        }



    }
}
