using Shared_Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Logic_Layer.Interface.DAL;
using Enums;

namespace Data_Access_Layer
{
	public class FlightDAL : Base, IFlightDAL
	{

		public bool CreateFlight(Flight flight)
		{
			string query = @"
                INSERT INTO Flights (DepartureTime, ArrivalTime, Status, Price, DepartureAirport, ArrivalAirport, AircraftID)
                VALUES (@DepartureTime, @ArrivalTime, @Status, @Price, @DepartureAirport, @ArrivalAirport, @AircraftID)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
						command.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
						command.Parameters.AddWithValue("@Status", (int)flight.FlightStatus);
						command.Parameters.AddWithValue("@Price", flight.Price);
						command.Parameters.AddWithValue("@DepartureAirport", flight.DepartureAirport.ID);
						command.Parameters.AddWithValue("@ArrivalAirport", flight.ArrivalAirport.ID);
						command.Parameters.AddWithValue("@AircraftID", flight.Plane.ID);

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

		public List<Flight> GetAllFlights()
		{
			List<Flight> flights = new List<Flight>();

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"
               SELECT 
                   f.ID, f.DepartureTime, f.ArrivalTime, f.Status, f.Price,
                   da.ID as DepartureAirportID, da.IATACode as DepartureIATACode, da.Name as DepartureName, da.City as DepartureCity, da.Country as DepartureCountry, da.TimeZone as DepartureTimeZone,
                   aa.ID as ArrivalAirportID, aa.IATACode as ArrivalIATACode, aa.Name as ArrivalName, aa.City as ArrivalCity, aa.Country as ArrivalCountry, aa.TimeZone as ArrivalTimeZone,
                   a.ID as AircraftID, a.RegistrationNumber, a.Model, a.Capacity, a.CurrentLocation, a.CurrentStatus,
                   c.ID as CurrentLocationID, c.IATACode as CurrentIATACode, c.Name as CurrentName, c.City as CurrentCity, c.Country as CurrentCountry, c.TimeZone as CurrentTimeZone
               FROM 
                   Flights f
               JOIN 
                   Airports da ON f.DepartureAirport = da.ID
               JOIN 
                   Airports aa ON f.ArrivalAirport = aa.ID
               JOIN 
                   Aircraft a ON f.AircraftID = a.ID
               JOIN
                   Airports c ON a.CurrentLocation = c.ID";

					SqlCommand command = new SqlCommand(query, connection);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Airport departureAirport = new Airport(
								reader.GetInt32(reader.GetOrdinal("DepartureAirportID")),
								reader.GetString(reader.GetOrdinal("DepartureIATACode")),
								reader.GetString(reader.GetOrdinal("DepartureName")),
								reader.GetString(reader.GetOrdinal("DepartureCity")),
								reader.GetString(reader.GetOrdinal("DepartureCountry")),
								reader.GetString(reader.GetOrdinal("DepartureTimeZone")));

							Airport arrivalAirport = new Airport(
								reader.GetInt32(reader.GetOrdinal("ArrivalAirportID")),
								reader.GetString(reader.GetOrdinal("ArrivalIATACode")),
								reader.GetString(reader.GetOrdinal("ArrivalName")),
								reader.GetString(reader.GetOrdinal("ArrivalCity")),
								reader.GetString(reader.GetOrdinal("ArrivalCountry")),
								reader.GetString(reader.GetOrdinal("ArrivalTimeZone")));

							Airport currentLocation = new Airport(
								reader.GetInt32(reader.GetOrdinal("CurrentLocationID")),
								reader.GetString(reader.GetOrdinal("CurrentIATACode")),
								reader.GetString(reader.GetOrdinal("CurrentName")),
								reader.GetString(reader.GetOrdinal("CurrentCity")),
								reader.GetString(reader.GetOrdinal("CurrentCountry")),
								reader.GetString(reader.GetOrdinal("CurrentTimeZone")));

							Plane plane = PlaneHelper.CreatePlane(
								(PlaneModel)reader.GetInt32(reader.GetOrdinal("Model")),
								reader.GetInt32(reader.GetOrdinal("AircraftID")),
								reader.GetString(reader.GetOrdinal("RegistrationNumber")),
								reader.GetInt32(reader.GetOrdinal("Capacity")),
								currentLocation,
								(PlaneStatus)reader.GetInt32(reader.GetOrdinal("CurrentStatus")));

							FlightStatus flightStatus = (FlightStatus)reader.GetInt32(reader.GetOrdinal("Status"));

                            Flight flight = new Flight(
								reader.GetInt32(reader.GetOrdinal("ID")),
								departureAirport,
								arrivalAirport,
								reader.GetDateTime(reader.GetOrdinal("DepartureTime")),
								reader.GetDateTime(reader.GetOrdinal("ArrivalTime")),
								Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("Price"))),
								plane,
								flightStatus);


                            flights.Add(flight);
						}
					}
				}
			}
			catch
			{
				throw;
			}

			return flights;
		}


		public List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate)
		{
			List<Flight> flights = new List<Flight>();

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"
						SELECT 
							f.ID, f.DepartureTime, f.ArrivalTime, f.Status, f.Price,
							da.ID as DepartureAirportID, da.IATACode as DepartureIATACode, da.Name as DepartureName, da.City as DepartureCity, da.Country as DepartureCountry, da.TimeZone as DepartureTimeZone,
							aa.ID as ArrivalAirportID, aa.IATACode as ArrivalIATACode, aa.Name as ArrivalName, aa.City as ArrivalCity, aa.Country as ArrivalCountry, aa.TimeZone as ArrivalTimeZone,
							a.ID as AircraftID, a.RegistrationNumber, a.Model, a.Capacity, a.CurrentLocation, a.CurrentStatus,
							c.ID as CurrentLocationID, c.IATACode as CurrentIATACode, c.Name as CurrentName, c.City as CurrentCity, c.Country as CurrentCountry, c.TimeZone as CurrentTimeZone
						FROM 
							Flights f
						JOIN 
							Airports da ON f.DepartureAirport = da.ID
						JOIN 
							Airports aa ON f.ArrivalAirport = aa.ID
						JOIN 
							Aircraft a ON f.AircraftID = a.ID
						JOIN
							Airports c ON a.CurrentLocation = c.ID
						WHERE 
							(da.City = @originCity AND aa.City = @destinationCity AND CAST(f.DepartureTime AS DATE) = @departureDate AND CAST(f.ArrivalTime AS DATE) >= @departureDate)";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@originCity", originCity);
					command.Parameters.AddWithValue("@destinationCity", destinationCity);
					command.Parameters.AddWithValue("@departureDate", departureDate.Date);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							int flightId = reader.GetInt32(reader.GetOrdinal("ID"));
							int aircraftId = reader.GetInt32(reader.GetOrdinal("AircraftID"));
							int aircraftCapacity = reader.GetInt32(reader.GetOrdinal("Capacity"));

							int ticketCount = GetTicketCountForFlight(flightId);

							if (ticketCount <= aircraftCapacity)
							{
								Airport departureAirport = new Airport(
								reader.GetInt32(reader.GetOrdinal("DepartureAirportID")),
								reader.GetString(reader.GetOrdinal("DepartureIATACode")),
								reader.GetString(reader.GetOrdinal("DepartureName")),
								reader.GetString(reader.GetOrdinal("DepartureCity")),
								reader.GetString(reader.GetOrdinal("DepartureCountry")),
								reader.GetString(reader.GetOrdinal("DepartureTimeZone")));

								Airport arrivalAirport = new Airport(
									reader.GetInt32(reader.GetOrdinal("ArrivalAirportID")),
									reader.GetString(reader.GetOrdinal("ArrivalIATACode")),
									reader.GetString(reader.GetOrdinal("ArrivalName")),
									reader.GetString(reader.GetOrdinal("ArrivalCity")),
									reader.GetString(reader.GetOrdinal("ArrivalCountry")),
									reader.GetString(reader.GetOrdinal("ArrivalTimeZone")));

								Airport currentLocation = new Airport(
									reader.GetInt32(reader.GetOrdinal("CurrentLocationID")),
									reader.GetString(reader.GetOrdinal("CurrentIATACode")),
									reader.GetString(reader.GetOrdinal("CurrentName")),
									reader.GetString(reader.GetOrdinal("CurrentCity")),
									reader.GetString(reader.GetOrdinal("CurrentCountry")),
									reader.GetString(reader.GetOrdinal("CurrentTimeZone")));

								Plane plane = PlaneHelper.CreatePlane(
									(PlaneModel)reader.GetInt32(reader.GetOrdinal("Model")),
									reader.GetInt32(reader.GetOrdinal("AircraftID")),
									reader.GetString(reader.GetOrdinal("RegistrationNumber")),
									reader.GetInt32(reader.GetOrdinal("Capacity")),
									currentLocation,
									(PlaneStatus)reader.GetInt32(reader.GetOrdinal("CurrentStatus")));

								FlightStatus flightStatus = (FlightStatus)reader.GetInt32(reader.GetOrdinal("Status"));

								Flight flight = new Flight(
									reader.GetInt32(reader.GetOrdinal("ID")),
									departureAirport,
									arrivalAirport,
									reader.GetDateTime(reader.GetOrdinal("DepartureTime")),
									reader.GetDateTime(reader.GetOrdinal("ArrivalTime")),
									Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("Price"))),
									plane,
									flightStatus);

								flights.Add(flight);
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}

			return flights;
		}





		public List<Flight> GetAllFlightsByLocationTimeDate(string originCity, string destinationCity, DateTime departureDate, DateTime returnDate)
		{
			List<Flight> flights = new List<Flight>();

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"
						SELECT 
							f.ID, f.DepartureTime, f.ArrivalTime, f.Status, f.Price,
							da.ID as DepartureAirportID, da.IATACode as DepartureIATACode, da.Name as DepartureName, da.City as DepartureCity, da.Country as DepartureCountry, da.TimeZone as DepartureTimeZone,
							aa.ID as ArrivalAirportID, aa.IATACode as ArrivalIATACode, aa.Name as ArrivalName, aa.City as ArrivalCity, aa.Country as ArrivalCountry, aa.TimeZone as ArrivalTimeZone,
							a.ID as AircraftID, a.RegistrationNumber, a.Model, a.Capacity, a.CurrentLocation, a.CurrentStatus,
							c.ID as CurrentLocationID, c.IATACode as CurrentIATACode, c.Name as CurrentName, c.City as CurrentCity, c.Country as CurrentCountry, c.TimeZone as CurrentTimeZone
						FROM 
							Flights f
						JOIN 
							Airports da ON f.DepartureAirport = da.ID
						JOIN 
							Airports aa ON f.ArrivalAirport = aa.ID
						JOIN 
							Aircraft a ON f.AircraftID = a.ID
						JOIN
							Airports c ON a.CurrentLocation = c.ID
						WHERE 
							(da.City = @originCity AND aa.City = @destinationCity AND CAST(f.DepartureTime AS DATE) = @departureDate AND CAST(f.ArrivalTime AS DATE) >= @departureDate)
							OR 
							(da.City = @destinationCity AND aa.City = @originCity AND CAST(f.DepartureTime AS DATE) = @returnDate AND CAST(f.ArrivalTime AS DATE) >= @returnDate)";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@originCity", originCity);
					command.Parameters.AddWithValue("@destinationCity", destinationCity);
					command.Parameters.AddWithValue("@departureDate", departureDate.Date);
					command.Parameters.AddWithValue("@returnDate", returnDate.Date);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							int flightId = reader.GetInt32(reader.GetOrdinal("ID"));
							int aircraftId = reader.GetInt32(reader.GetOrdinal("AircraftID"));
							int aircraftCapacity = reader.GetInt32(reader.GetOrdinal("Capacity"));

							int ticketCount = GetTicketCountForFlight(flightId);

							if (ticketCount <= aircraftCapacity)
							{
								Airport departureAirport = new Airport(
									reader.GetInt32(reader.GetOrdinal("DepartureAirportID")),
									reader.GetString(reader.GetOrdinal("DepartureIATACode")),
									reader.GetString(reader.GetOrdinal("DepartureName")),
									reader.GetString(reader.GetOrdinal("DepartureCity")),
									reader.GetString(reader.GetOrdinal("DepartureCountry")),
									reader.GetString(reader.GetOrdinal("DepartureTimeZone")));

								Airport arrivalAirport = new Airport(
									reader.GetInt32(reader.GetOrdinal("ArrivalAirportID")),
									reader.GetString(reader.GetOrdinal("ArrivalIATACode")),
									reader.GetString(reader.GetOrdinal("ArrivalName")),
									reader.GetString(reader.GetOrdinal("ArrivalCity")),
									reader.GetString(reader.GetOrdinal("ArrivalCountry")),
									reader.GetString(reader.GetOrdinal("ArrivalTimeZone")));

								Airport currentLocation = new Airport(
									reader.GetInt32(reader.GetOrdinal("CurrentLocationID")),
									reader.GetString(reader.GetOrdinal("CurrentIATACode")),
									reader.GetString(reader.GetOrdinal("CurrentName")),
									reader.GetString(reader.GetOrdinal("CurrentCity")),
									reader.GetString(reader.GetOrdinal("CurrentCountry")),
									reader.GetString(reader.GetOrdinal("CurrentTimeZone")));

								Plane plane = PlaneHelper.CreatePlane(
									(PlaneModel)reader.GetInt32(reader.GetOrdinal("Model")),
									reader.GetInt32(reader.GetOrdinal("AircraftID")),
									reader.GetString(reader.GetOrdinal("RegistrationNumber")),
									reader.GetInt32(reader.GetOrdinal("Capacity")),
									currentLocation,
									(PlaneStatus)reader.GetInt32(reader.GetOrdinal("CurrentStatus")));

								FlightStatus flightStatus = (FlightStatus)reader.GetInt32(reader.GetOrdinal("Status"));

								Flight flight = new Flight(
									reader.GetInt32(reader.GetOrdinal("ID")),
									departureAirport,
									arrivalAirport,
									reader.GetDateTime(reader.GetOrdinal("DepartureTime")),
									reader.GetDateTime(reader.GetOrdinal("ArrivalTime")),
									Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("Price"))),
									plane,
									flightStatus);

								flights.Add(flight);
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}

			return flights;
		}


		private int GetTicketCountForFlight(int flightId)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string ticketCountQuery = "SELECT COUNT(*) FROM Tickets WHERE FlightID = @flightId";
					using (SqlCommand ticketCountCommand = new SqlCommand(ticketCountQuery, connection))
					{
						ticketCountCommand.Parameters.AddWithValue("@flightId", flightId);
						return (int)ticketCountCommand.ExecuteScalar();
					}
				}
			}
			catch
			{
				throw;
			}
		}


		public Flight GetFlightByID(int flightId)
		{
			Flight flight = null;

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"
                       SELECT 
                           f.ID, f.DepartureTime, f.ArrivalTime, f.Status, f.Price,
                           da.ID as DepartureAirportID, da.IATACode as DepartureIATACode, da.Name as DepartureName, da.City as DepartureCity, da.Country as DepartureCountry, da.TimeZone as DepartureTimeZone,
                           aa.ID as ArrivalAirportID, aa.IATACode as ArrivalIATACode, aa.Name as ArrivalName, aa.City as ArrivalCity, aa.Country as ArrivalCountry, aa.TimeZone as ArrivalTimeZone,
                           a.ID as AircraftID, a.RegistrationNumber, a.Model, a.Capacity, a.CurrentLocation, a.CurrentStatus,
                           c.ID as CurrentLocationID, c.IATACode as CurrentIATACode, c.Name as CurrentName, c.City as CurrentCity, c.Country as CurrentCountry, c.TimeZone as CurrentTimeZone
                       FROM 
                           Flights f
                       JOIN 
                           Airports da ON f.DepartureAirport = da.ID
                       JOIN 
                           Airports aa ON f.ArrivalAirport = aa.ID
                       JOIN 
                           Aircraft a ON f.AircraftID = a.ID
                       JOIN
                           Airports c ON a.CurrentLocation = c.ID
                       WHERE 
                           f.ID = @FlightId";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@FlightId", flightId);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							Airport departureAirport = new Airport(
								reader.GetInt32(reader.GetOrdinal("DepartureAirportID")),
								reader.GetString(reader.GetOrdinal("DepartureIATACode")),
								reader.GetString(reader.GetOrdinal("DepartureName")),
								reader.GetString(reader.GetOrdinal("DepartureCity")),
								reader.GetString(reader.GetOrdinal("DepartureCountry")),
								reader.GetString(reader.GetOrdinal("DepartureTimeZone")));

							Airport arrivalAirport = new Airport(
								reader.GetInt32(reader.GetOrdinal("ArrivalAirportID")),
								reader.GetString(reader.GetOrdinal("ArrivalIATACode")),
								reader.GetString(reader.GetOrdinal("ArrivalName")),
								reader.GetString(reader.GetOrdinal("ArrivalCity")),
								reader.GetString(reader.GetOrdinal("ArrivalCountry")),
								reader.GetString(reader.GetOrdinal("ArrivalTimeZone")));

							Airport currentLocation = new Airport(
								reader.GetInt32(reader.GetOrdinal("CurrentLocationID")),
								reader.GetString(reader.GetOrdinal("CurrentIATACode")),
								reader.GetString(reader.GetOrdinal("CurrentName")),
								reader.GetString(reader.GetOrdinal("CurrentCity")),
								reader.GetString(reader.GetOrdinal("CurrentCountry")),
								reader.GetString(reader.GetOrdinal("CurrentTimeZone")));

							Plane plane = PlaneHelper.CreatePlane(
								(PlaneModel)reader.GetInt32(reader.GetOrdinal("Model")),
								reader.GetInt32(reader.GetOrdinal("AircraftID")),
								reader.GetString(reader.GetOrdinal("RegistrationNumber")),
								reader.GetInt32(reader.GetOrdinal("Capacity")),
								currentLocation,
								(PlaneStatus)reader.GetInt32(reader.GetOrdinal("CurrentStatus")));

							FlightStatus flightStatus = (FlightStatus)reader.GetInt32(reader.GetOrdinal("Status"));

							flight = new Flight(
								reader.GetInt32(reader.GetOrdinal("ID")),
								departureAirport,
								arrivalAirport,
								reader.GetDateTime(reader.GetOrdinal("DepartureTime")),
								reader.GetDateTime(reader.GetOrdinal("ArrivalTime")),
								Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("Price"))),
								plane,
								flightStatus);
						}
					}
				}
			}
			catch
			{
				throw;
			}

			return flight;
		}



		public double GetFlightPrice(int flightID, SeatModel seatModel)
		{
			string query = "SELECT Price FROM Flights WHERE ID = @FlightID";
			double basePrice = 0;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@FlightID", flightID);
						object result = command.ExecuteScalar();
						if (result != null && result != DBNull.Value)
						{
							basePrice = Convert.ToDouble(result);
						}
						else
						{
							throw new ArgumentException("Flight ID not found in the database.");
						}
					}
				}
				catch
				{
					throw;
				}
			}

			// Apply price increase based on seat model
			switch (seatModel)
			{
				case SeatModel.First:
					return basePrice * 1.30; // Increase by 30% for First class
				case SeatModel.Business:
					return basePrice * 1.14; // Increase by 14% for Business class
				case SeatModel.Economy:
					return basePrice; // Base price for Economy class
				default:
					throw new ArgumentException("Invalid seat model.");
			}
		}



		public bool UpdateFlight(Flight flight)
		{
			string query = @"
                UPDATE Flights 
                SET DepartureTime = @DepartureTime, 
                    ArrivalTime = @ArrivalTime, 
                    Status = @Status, 
                    Price = @Price, 
                    DepartureAirport = @DepartureAirport, 
                    ArrivalAirport = @ArrivalAirport, 
                    AircraftID = @AircraftID
                WHERE ID = @FlightID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
						command.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
						command.Parameters.AddWithValue("@Status", (int)flight.FlightStatus);
						command.Parameters.AddWithValue("@Price", flight.Price);
						command.Parameters.AddWithValue("@DepartureAirport", flight.DepartureAirport.ID);
						command.Parameters.AddWithValue("@ArrivalAirport", flight.ArrivalAirport.ID);
						command.Parameters.AddWithValue("@AircraftID", flight.Plane.ID);
						command.Parameters.AddWithValue("@FlightID", flight.FlightID);

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


		public bool DeleteFlightByID(int id)
		{
			string query = "DELETE FROM Flights WHERE ID = @FlightID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@FlightID", id);

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
