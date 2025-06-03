namespace Shared_Classes
{
    public class Flight
    {
        public int FlightID { get; private set; }
        public Airport DepartureAirport { get; private set; }
        public Airport ArrivalAirport { get; private set; }

        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public double Price { get; private set; } // base or minimum price.
        // the price for Economy is the base price
        // the price for Business is 40% more than the base price
        // the price for First class is 60% more than the base price
        // calculated by the system when the user selects seat.

        public Plane Plane { get; private set; }

        public FlightStatus FlightStatus { get; private set; }


        // create flight to db 
        public Flight(Airport origin, Airport destination, DateTime departureTime, DateTime arrivalTime, double price, Plane plane, FlightStatus flightStatus)
        {
            if (origin == null)
                throw new ArgumentNullException(nameof(origin), "Origin airport must be provided.");

            if (destination == null)
                throw new ArgumentNullException(nameof(destination), "Destination airport must be provided.");

            if (departureTime >= arrivalTime)
                throw new ArgumentException("Departure time must be before arrival time.");

            if (price <= 0)
                throw new ArgumentException("Price must be a positive number.", nameof(price));

            if (plane == null)
                throw new ArgumentNullException(nameof(plane), "Plane must be provided.");


            this.DepartureAirport = origin;
            this.ArrivalAirport = destination;
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.Price = price;
            this.Plane = plane;
            this.FlightStatus = flightStatus;
        }



        // get from DB
        public Flight(int flightID, Airport origin, Airport destination, DateTime departureTime, DateTime arrivalTime, double price, Plane plane, FlightStatus flightStatus)
        {
            if (flightID < 0)
                throw new ArgumentException("Flight ID must be a non-negative integer.", nameof(flightID));

            if (origin == null)
                throw new ArgumentNullException(nameof(origin), "Origin airport must be provided.");

            if (destination == null)
                throw new ArgumentNullException(nameof(destination), "Destination airport must be provided.");

            if (departureTime >= arrivalTime)
                throw new ArgumentException("Departure time must be before arrival time.");

            if (price <= 0)
                throw new ArgumentException("Price must be a positive number.", nameof(price));

            if (plane == null)
                throw new ArgumentNullException(nameof(plane), "Plane must be provided.");


            this.FlightID = flightID;
            this.DepartureAirport = origin;
            this.ArrivalAirport = destination;
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.Price = price;
            this.Plane = plane;
            this.FlightStatus = flightStatus;
        }




    }
}
