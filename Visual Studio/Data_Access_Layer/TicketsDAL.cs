using Logic_Layer.Interface.DAL;
using Microsoft.Data.SqlClient;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Data_Access_Layer
{
    public class TicketsDAL : Base, ITicketsDAL
    {
        public TicketsDAL()
        {

        }

        // no seat selection ( Economy seat 24hrs before takeoff user will select random seat )
        public bool CreateTicket(int flightID, int userID)
        {
            string query = "INSERT INTO Tickets (FlightID, PassengerID) VALUES (@FlightID, @UserID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FlightID", flightID);
                        command.Parameters.AddWithValue("@UserID", userID);

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


        public bool CreateTicket(int flightID, int userID, SeatModel seatModel, int seatRow, char seatColumn)
        {
            string query = "INSERT INTO Tickets (FlightID, PassengerID, Class, SeatRow, SeatColumn) VALUES (@FlightID, @UserID, @SeatModel, @SeatRow, @SeatColumn)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FlightID", flightID);
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@SeatModel", (object)seatModel ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SeatRow", (object)seatRow ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SeatColumn", (object)seatColumn ?? DBNull.Value);

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




        public Ticket GetTicketByID(int ticketId)
        {
            string query = "SELECT * FROM Tickets WHERE ID = @TicketId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TicketId", ticketId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                char? seatColumnChar = reader["SeatColumn"] is DBNull ? null : (char?)reader["SeatColumn"].ToString()[0];
                                return new Ticket(
                                    ticketID: (int)reader["ID"],
                                    userID: (int)reader["PassengerID"],
                                    flightID: (int)reader["FlightID"],
                                    seatModel: reader["Class"] is DBNull ? null : (SeatModel?)(int)reader["Class"], // Nullable SeatModel
                                    seatRow: reader["SeatRow"] is DBNull ? null : (int?)reader["SeatRow"], // Nullable int
                                    seatColumn: seatColumnChar // Nullable char
                                );
                            }
                            else
                            {
                                // Ticket not found
                                return null;
                            }
                        }
                    }
                } 
                catch 
                {
                    throw;
                }
            }
        }

        public List<Ticket> GetAllTicketsByPassengerID(int passengerId)
        {
            List<Ticket> tickets = new List<Ticket>();
            string query = "SELECT * FROM Tickets WHERE PassengerID = @PassengerId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PassengerId", passengerId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                char? seatColumnChar = reader["SeatColumn"] is DBNull ? null : (char?)reader["SeatColumn"].ToString()[0];
                                tickets.Add(new Ticket(
                                    ticketID: (int)reader["ID"],
                                    userID: (int)reader["PassengerID"],
                                    flightID: (int)reader["FlightID"],
                                    seatModel: reader["Class"] is DBNull ? null : (SeatModel?)(int)reader["Class"], // Nullable SeatModel
                                    seatRow: reader["SeatRow"] is DBNull ? null : (int?)reader["SeatRow"], // Nullable int
                                    seatColumn: seatColumnChar // Nullable char
                                ));
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            return tickets;
        }


        public List<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            string query = "SELECT * FROM Tickets";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                char? seatColumnChar = reader["SeatColumn"] is DBNull ? null : (char?)reader["SeatColumn"].ToString()[0];

                                tickets.Add(new Ticket(
                                    ticketID: (int)reader["ID"],
                                    userID: (int)reader["PassengerID"],
                                    flightID: (int)reader["FlightID"],
                                    seatModel: reader["Class"] is DBNull ? null : (SeatModel?)(int)reader["Class"], // Nullable SeatModel
                                    seatRow: reader["SeatRow"] is DBNull ? null : (int?)reader["SeatRow"], // Nullable int
                                    seatColumn: seatColumnChar // Nullable char
                                ));
                            }
                        }
                    }
                }
                catch 
                {
                    throw;
                }
            }
            return tickets;
        }


        public List<Ticket> GetAllTicketsByFlightID(int flightId)
        {
            List<Ticket> tickets = new List<Ticket>();
            string query = "SELECT * FROM Tickets WHERE FlightID = @FlightId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FlightId", flightId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                char? seatColumnChar = reader["SeatColumn"] is DBNull ? null : (char?)reader["SeatColumn"].ToString()[0];

                                tickets.Add(new Ticket(
                                    ticketID: (int)reader["ID"], // Non-nullable int
                                    userID: (int)reader["PassengerID"], // Non-nullable int
                                    flightID: (int)reader["FlightID"], // Non-nullable int
                                    seatModel: reader["Class"] is DBNull ? null : (SeatModel?)(int)reader["Class"], // Nullable SeatModel
                                    seatRow: reader["SeatRow"] is DBNull ? null : (int?)reader["SeatRow"], // Nullable int
                                    seatColumn: seatColumnChar // Nullable char
                                ));
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            return tickets;
        }


        public bool UpdateTicket(Ticket ticket)
        {
            string query = "UPDATE Tickets SET SeatRow = @SeatRow, SeatColumn = @SeatColumn WHERE ID = @TicketId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TicketId", ticket.ID);
                        command.Parameters.AddWithValue("@SeatRow", ticket.SeatRow);
                        command.Parameters.AddWithValue("@SeatColumn", ticket.SeatColumn);

                        int result = command.ExecuteNonQuery();
                        return result > 0; // Returns true if at least one record was updated
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool DeleteTicketByID(int ticketId)
        {
            string query = "DELETE FROM Tickets WHERE ID = @TicketId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TicketId", ticketId);

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
