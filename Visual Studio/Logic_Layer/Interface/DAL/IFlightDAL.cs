using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Classes;
using Enums;

namespace Logic_Layer.Interface.DAL
{
    public interface IFlightDAL
    {
        bool CreateFlight(Flight flight);

        double GetFlightPrice(int flightID, SeatModel seatModel);
        Flight GetFlightByID(int flightId);
        List<Flight> GetAllFlights();
        List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate, DateTime returnDate);
        List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate);

		bool UpdateFlight(Flight flight);

        bool DeleteFlightByID(int id);
    }
}
