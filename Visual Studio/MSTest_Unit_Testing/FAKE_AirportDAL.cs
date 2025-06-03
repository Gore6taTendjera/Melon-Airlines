using Logic_Layer.Interface.DAL;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uMSTest_Unit_Testing
{
    public class FAKE_AirportDAL : IAirportDAL
    {
        private Dictionary<int, Airport> _airports = new Dictionary<int, Airport>
        {
            {1, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London")},
            {2, new Airport(2, "JFK", "John F. Kennedy", "New York", "USA", "USA/New_York")}
        };

        public bool CreateAirport(Airport airport)
        {
            if (airport != null)
            {
                _airports.Add(airport.ID, airport);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Airport GetAirportByID(int id)
        {
            foreach (var aiport in _airports.Values)
            {
                if (aiport.ID == id)
                    return aiport;
            }
            return null;
        }

        public bool DeleteAirportByID(int id)
        {
            if (_airports.ContainsKey(id))
            {
                _airports.Remove(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Airport> GetAllAirports()
        {
            return new List<Airport>(_airports.Values);
        }

    }
}
