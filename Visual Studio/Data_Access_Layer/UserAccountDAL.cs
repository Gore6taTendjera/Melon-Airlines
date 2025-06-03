using Shared_Classes;
using Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Interface.DAL;

namespace Data_Access_Layer
{
	public class UserAccountDAL : Base, IUserAccountDAL
	{

        public bool UsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool InsertUser(User user)
        {
            string query = "INSERT INTO Users (Username, Password, name, MiddleName, Surname, Gender, BirthDate, BirthPlace, Email, Nationality, UserType) VALUES (@Username, @Password, @Name, @MiddleName, @Surname, @Gender, @BirthDate, @BirthPlace, @Email, @Nationality, @UserType)";


            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password); // hashed password
                command.Parameters.AddWithValue("@Name", user.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@MiddleName", user.MiddleName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Surname", user.Surname ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BirthDate", user.BirthDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BirthPlace", user.BirthPlace ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Nationality", user.Nationality ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UserType", user.UserType);
                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting user: " + ex.Message);
                    return false;
                }
            }
        }

        public List<User> GetAllUsers()
		{
			List<User> users = new List<User>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string sql = "SELECT * FROM Users";
				SqlCommand command = new SqlCommand(sql, connection);

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						User user = new User(
							reader.GetInt32(0), // UserID
							reader.IsDBNull(1) ? null : reader.GetString(1), // Name
							reader.IsDBNull(2) ? null : reader.GetString(2), // MiddleName
							reader.IsDBNull(3) ? null : reader.GetString(3), // Surname
							reader.IsDBNull(4) ? Gender.NONE : (Gender)reader.GetInt32(4), // Gender
							reader.IsDBNull(5) ? DateOnly.MinValue : DateOnly.FromDateTime(reader.GetDateTime(5)), // BirthDate
							reader.IsDBNull(6) ? null : reader.GetString(6), // BirthPlace
							reader.IsDBNull(7) ? null : reader.GetString(7), // Username
							reader.IsDBNull(8) ? null : reader.GetString(8), // Password
							reader.IsDBNull(9) ? null : reader.GetString(9), // Email
							reader.IsDBNull(10) ? Nationality.NONE : (Nationality)reader.GetInt32(10), // Nationality
							(UserType)reader.GetInt32(11) // UserType
						);

						users.Add(user);
					}
				}
				connection.Close();
			}

			return users;
		}


		public User GetUserById(int id)
		{
			string query = "SELECT * FROM Users WHERE UserID = @UserID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@UserID", id);

				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						User user = new User(
							reader.GetInt32(0), // UserID
							reader.IsDBNull(1) ? null : reader.GetString(1), // Name
							reader.IsDBNull(2) ? null : reader.GetString(2), // MiddleName
							reader.IsDBNull(3) ? null : reader.GetString(3), // Surname
							reader.IsDBNull(4) ? Gender.NONE : (Gender)reader.GetInt32(4), // Gender
							reader.IsDBNull(5) ? DateOnly.MinValue : DateOnly.FromDateTime(reader.GetDateTime(5)), // BirthDate
							reader.IsDBNull(6) ? null : reader.GetString(6), // BirthPlace
							reader.IsDBNull(7) ? null : reader.GetString(7), // Username
							reader.IsDBNull(8) ? null : reader.GetString(8), // Password
							reader.IsDBNull(9) ? null : reader.GetString(9), // Email
							reader.IsDBNull(10) ? Nationality.NONE : (Nationality)reader.GetInt32(10), // Nationality
							(UserType)reader.GetInt32(11) // UserType
						);

						return user;
					}
					else
					{
						// user not found
						return null;
					}
				}
				catch
				{
					throw;
				}
			}
		}



		public User Authenticate(string username)
		{
			string query = @"SELECT * FROM Users WHERE Username = @Username";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				connection.Open();

				command.Parameters.AddWithValue("@Username", username);

				try
				{
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						User user = new User(
							reader.GetInt32(reader.GetOrdinal("UserID")),
							reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
							reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? null : reader.GetString(reader.GetOrdinal("MiddleName")),
							reader.IsDBNull(reader.GetOrdinal("Surname")) ? null : reader.GetString(reader.GetOrdinal("Surname")),
							reader.IsDBNull(reader.GetOrdinal("Gender")) ? Gender.NONE : (Gender)reader.GetInt32(reader.GetOrdinal("Gender")),
							reader.IsDBNull(reader.GetOrdinal("BirthDate")) ? DateOnly.MinValue : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("BirthDate"))),
							reader.IsDBNull(reader.GetOrdinal("BirthPlace")) ? null : reader.GetString(reader.GetOrdinal("BirthPlace")),
							username,
							reader.GetString(reader.GetOrdinal("Password")),
							reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
							reader.IsDBNull(reader.GetOrdinal("Nationality")) ? Nationality.NONE : (Nationality)reader.GetInt32(reader.GetOrdinal("Nationality")),
							(UserType)reader.GetInt32(reader.GetOrdinal("UserType"))
						);

						return user;
					}

					// User not found
					return null;
				}
				catch
				{
					throw;
				}
			}
		}




		public bool UpdateUser(User user)
		{
			string query = @"UPDATE Users 
                    SET Name = @Name, 
                        MiddleName = @MiddleName, 
                        Surname = @Surname, 
                        Gender = @Gender, 
                        BirthDate = @BirthDate, 
                        BirthPlace = @BirthPlace, 
                        Email = @Email, 
                        Nationality = @Nationality
                    WHERE UserID = @UserID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@UserID", user.ID);
				command.Parameters.AddWithValue("@Name", user.Name ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@MiddleName", user.MiddleName ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@Surname", user.Surname ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@Gender", user.Gender.HasValue ? (int)user.Gender : DBNull.Value);
				command.Parameters.AddWithValue("@BirthDate", user.BirthDate ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@BirthPlace", user.BirthPlace ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@Nationality", user.Nationality.HasValue ? (int)user.Nationality : DBNull.Value);

				try
				{
					connection.Open();
					int rowsAffected = command.ExecuteNonQuery();
					return rowsAffected > 0;
				}
				catch
				{
					throw;
				}
			}
		}

		public bool DeleteUser(int id)
		{
			string query = "DELETE FROM Users WHERE UserID = @UserID";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@UserID", id);

				try
				{
					connection.Open();
					int rowsAffected = command.ExecuteNonQuery();
					return rowsAffected > 0;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error deleting user: " + ex.Message);
					return false;
				}
			}
		}
	}
}