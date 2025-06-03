using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using DTOs;

namespace Logic_Layer.Interface.LL
{
    public interface IFlightService
    {
        bool CreateFlight(Airport origin, Airport destination, DateTime takeoff, DateTime arrival, double price, Plane plane, FlightStatus flightStatus);


        Flight GetFlightByID(int id);
        FlightDTO GetFlightByIdDTO(int id);
        List<Flight> GetAllFlights();
        double GetFlightPrice(int flightID, SeatModel seatModel);
        List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate, DateTime returnDate);
        List<FlightDTO> GetAllFlightsByLocationTimeDateDTO(string originCity, string destinationCity, DateTime departureDate, DateTime? returnDate);


        bool UpdateFlight(Flight flight);


        bool DeleteFlightByID(int id);
    }
}
