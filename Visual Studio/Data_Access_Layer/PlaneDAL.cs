using Logic_Layer;
using Logic_Layer.Interface.DAL;
using Microsoft.Data.SqlClient;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Data_Access_Layer;

namespace Data_Access_Layer
{
    public class PlaneDAL : Base, IPlaneDAL
    {
        public bool CreateNewPlane(Plane plane)
        {
            string query = @"INSERT INTO Aircraft (RegistrationNumber, Model, Capacity, CurrentLocation, CurrentStatus)
                             VALUES (@RegistrationNumber, @Model, @Capacity, @CurrentLocation, @CurrentStatus)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegistrationNumber", plane.RegistrationNumber);
                        command.Parameters.AddWithValue("@Model", plane.PlaneModel);
                        command.Parameters.AddWithValue("@Capacity", plane.Capacity);
                        command.Parameters.AddWithValue("@CurrentLocation", plane.CurrentLocation.ID); // Assuming CurrentLocation is an Airport object with an ID
                        command.Parameters.AddWithValue("@CurrentStatus", plane.CurrentStatus);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch
                {
                    // Optionally handle exceptions, log, etc.
                    throw;
                }
            }
        }

        public Plane GetPlaneByFlightID(int flightID)
        {
            Plane plane = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Airport is needed becauase we need to know where the Plane is located
                    // and calculate the timezone and many other things...
                    string query = @"SELECT Aircraft.ID, Aircraft.RegistrationNumber, Aircraft.Model, Aircraft.Capacity, 
                                    Aircraft.CurrentLocation, Aircraft.CurrentStatus,
                                    Airports.ID AS AirportID, Airports.IATACode, Airports.Name, 
                                    Airports.City, Airports.Country, Airports.TimeZone
                             FROM Aircraft
                             INNER JOIN Flights ON Aircraft.ID = Flights.AircraftID
                             INNER JOIN Airports ON Aircraft.CurrentLocation = Airports.ID
                             WHERE Flights.ID = @FlightID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FlightID", flightID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = (int)reader["ID"];
                                string registrationNumber = (string)reader["RegistrationNumber"];
                                PlaneModel model = (PlaneModel)reader["Model"];
                                int capacity = (int)reader["Capacity"];

                                int airportID = (int)reader["AirportID"];
                                string iataCode = (string)reader["IATACode"];
                                string airportName = (string)reader["Name"];
                                string city = (string)reader["City"];
                                string country = (string)reader["Country"];
                                string timeZone = (string)reader["TimeZone"];

                                Airport currentLocation = new Airport(airportID, iataCode, airportName, city, country, timeZone);

                                PlaneStatus status = (PlaneStatus)reader["CurrentStatus"];

                                plane = PlaneHelper.CreatePlane(model, id, registrationNumber, capacity, currentLocation, status);

                            }
                        }
                    }
                }
                catch 
                {
                    throw;
                }
            }

            return plane;
        }

        public List<Plane> GetAllPlanes()
        {
            List<Plane> planes = new List<Plane>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT Aircraft.ID, Aircraft.RegistrationNumber, Aircraft.Model, Aircraft.Capacity, 
                     Aircraft.CurrentLocation, Aircraft.CurrentStatus,
                     Airports.ID AS AirportID, Airports.IATACode, Airports.Name, 
                     Airports.City, Airports.Country, Airports.TimeZone
                     FROM Aircraft
                     INNER JOIN Airports ON Aircraft.CurrentLocation = Airports.ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = (int)reader["ID"];
                                string registrationNumber = (string)reader["RegistrationNumber"];
                                PlaneModel model = (PlaneModel)reader["Model"];
                                int capacity = (int)reader["Capacity"];

                                int airportID = (int)reader["AirportID"];
                                string iataCode = (string)reader["IATACode"];
                                string airportName = (string)reader["Name"];
                                string city = (string)reader["City"];
                                string country = (string)reader["Country"];
                                string timeZone = (string)reader["TimeZone"];

                                Airport currentLocation = new Airport(airportID, iataCode, airportName, city, country, timeZone);

                                PlaneStatus status = (PlaneStatus)reader["CurrentStatus"];

                                Plane plane = PlaneHelper.CreatePlane(model, id, registrationNumber, capacity, currentLocation, status);
                                planes.Add(plane);
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            return planes;
        }

        public Plane GetPlaneByID(int id)
        {
            Plane plane = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT Aircraft.ID, Aircraft.RegistrationNumber, Aircraft.Model, Aircraft.Capacity, 
                             Aircraft.CurrentLocation, Aircraft.CurrentStatus,
                             Airports.ID AS AirportID, Airports.IATACode, Airports.Name, 
                             Airports.City, Airports.Country, Airports.TimeZone
                             FROM Aircraft
                             INNER JOIN Airports ON Aircraft.CurrentLocation = Airports.ID
                             WHERE Aircraft.ID = @PlaneID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PlaneID", id);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string registrationNumber = (string)reader["RegistrationNumber"];
                                PlaneModel model = (PlaneModel)reader["Model"];
                                int capacity = (int)reader["Capacity"];

                                int airportID = (int)reader["AirportID"];
                                string iataCode = (string)reader["IATACode"];
                                string airportName = (string)reader["Name"];
                                string city = (string)reader["City"];
                                string country = (string)reader["Country"];
                                string timeZone = (string)reader["TimeZone"];

                                Airport currentLocation = new Airport(airportID, iataCode, airportName, city, country, timeZone);

                                PlaneStatus status = (PlaneStatus)reader["CurrentStatus"];

                                plane = PlaneHelper.CreatePlane(model, id, registrationNumber, capacity, currentLocation, status);
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            return plane;
        }


        public bool UpdatePlane(Plane plane)
        {
            string query = @"UPDATE Aircraft 
                             SET RegistrationNumber = @RegistrationNumber, 
                                 Model = @Model, 
                                 Capacity = @Capacity, 
                                 CurrentLocation = @CurrentLocation, 
                                 CurrentStatus = @CurrentStatus 
                             WHERE ID = @PlaneId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegistrationNumber", plane.RegistrationNumber);
                        command.Parameters.AddWithValue("@Model", plane.PlaneModel);
                        command.Parameters.AddWithValue("@Capacity", plane.Capacity);
                        command.Parameters.AddWithValue("@CurrentLocation", plane.CurrentLocation.ID);
                        command.Parameters.AddWithValue("@CurrentStatus", plane.CurrentStatus);
                        command.Parameters.AddWithValue("@PlaneId", plane.ID);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool DeletePlaneByID(int planeId)
        {
            string query = "DELETE FROM Aircraft WHERE ID = @PlaneId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PlaneId", planeId);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }



    }
}
