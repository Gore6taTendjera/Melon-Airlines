using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Classes
{
    public class Airport
    {
        public int ID { get; private set; }
        public string IATACode { get; private set; }
        public string AirportName { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string TimeZone { get; private set; }


        // db get
        public Airport(int id, string iataCode, string airportName, string city, string country, string timeZone)
        {
            if (id < 0)
                throw new ArgumentException("ID must be a non-negative integer.", nameof(id));

            if (string.IsNullOrWhiteSpace(iataCode))
                throw new ArgumentException("IATA Code must not be null or empty.", nameof(iataCode));

            if (string.IsNullOrWhiteSpace(airportName))
                throw new ArgumentException("Airport Name must not be null or empty.", nameof(airportName));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City must not be null or empty.", nameof(city));

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country must not be null or empty.", nameof(country));

            if (string.IsNullOrWhiteSpace(timeZone))
                throw new ArgumentException("Time Zone must not be null or empty.", nameof(timeZone));

            ID = id;
            IATACode = iataCode;
            AirportName = airportName;
            City = city;
            Country = country;
            TimeZone = timeZone;
        }


    }
}
