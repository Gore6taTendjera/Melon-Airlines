using Logic_Layer;
using Logic_Layer.Interface.DAL;
using Microsoft.Data.SqlClient;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class AirportDAL : Base, IAirportDAL
    {
        public AirportDAL()
        {
            
        }

        public bool CreateAirport(Airport airport)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Airports (IATACode, Name, City, Country, TimeZone) " +
                                   "VALUES (@IATACode, @Name, @City, @Country, @TimeZone)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IATACode", airport.IATACode);
                    command.Parameters.AddWithValue("@Name", airport.AirportName);
                    command.Parameters.AddWithValue("@City", airport.City);
                    command.Parameters.AddWithValue("@Country", airport.Country);
                    command.Parameters.AddWithValue("@TimeZone", airport.TimeZone);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<Airport> GetAllAirports()
        {
            List<Airport> airports = new List<Airport>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Airports";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string iataCode = reader.GetString(1);
                        string name = reader.GetString(2);
                        string city = reader.GetString(3);
                        string country = reader.GetString(4);
                        string timeZone = reader.GetString(5);

                        Airport airport = new Airport(id, iataCode, name, city, country, timeZone);
                        airports.Add(airport);
                    }
                }
            }
            catch
            {
                throw;
            }

            return airports;
        }



        public Airport GetAirportByID(int ID)
        {
            Airport airport = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Airports WHERE ID = @AirportId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@AirportId", ID);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string iataCode = reader.GetString(1);
                        string name = reader.GetString(2);
                        string city = reader.GetString(3);
                        string country = reader.GetString(4);
                        string timeZone = reader.GetString(5);

                        airport = new Airport(ID,iataCode, name, city, country, timeZone);
                    }
                }
            }
            catch
            {
                throw;
            }

            return airport;
        }

        public bool DeleteAirportByID(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Airports WHERE ID = @AirportId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@AirportId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
