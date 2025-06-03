using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services
{
    public class AirportService: IAirportService
    {
        private readonly IAirportDAL _airportDAL;

        public AirportService(IAirportDAL airportDAL)
        {
            _airportDAL = airportDAL;
        }

        public bool CreateAirport(Airport airport)
        {
            return _airportDAL.CreateAirport(airport);
        }

        public List<Airport> GetAllAirports()
        {
            return _airportDAL.GetAllAirports();
        }

        public Airport GetAirportByID(int ID)
        {
            return _airportDAL.GetAirportByID(ID);
        }

        public bool DeleteAirportByID(int ID)
        {
            return _airportDAL.DeleteAirportByID(ID);
        }
    }
}
