using Shared_Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Interface.DAL;

namespace Data_Access_Layer
{
	public class DocumentDAL : Base, IIDDAL, IPassportDAL
	{
		public DocumentDAL() { }

		public bool CreateDocumentID(DocumentID documentID)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"INSERT INTO IDDocuments (UserID, IDNumber, DateOfIssue, DateOfExpire)
                                     VALUES (@UserID, @IDNumber, @DateOfIssue, @DateOfExpire)";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@UserID", documentID.UserId);
					command.Parameters.AddWithValue("@IDNumber", documentID.IDNumber);
					command.Parameters.AddWithValue("@DateOfIssue", documentID.DateOfIssue);
					command.Parameters.AddWithValue("@DateOfExpire", documentID.DateOfExpire);

					int rowsAffected = command.ExecuteNonQuery();

					connection.Close();

					return rowsAffected > 0;
				}
			}
			catch
			{
				return false;
			}
		}


		public bool CreateDocumentPassport(DocumentPassport documentPassport)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"INSERT INTO PassportDocuments (UserID, PassportNumber, DateOfIssue, DateOfExpire)
                                     VALUES (@UserID, @PassportNumber, @DateOfIssue, @DateOfExpire)";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@UserID", documentPassport.UserId);
					command.Parameters.AddWithValue("@PassportNumber", documentPassport.PassportNumber);
					command.Parameters.AddWithValue("@DateOfIssue", documentPassport.DateOfIssue);
					command.Parameters.AddWithValue("@DateOfExpire", documentPassport.DateOfExpire);

					int rowsAffected = command.ExecuteNonQuery();

					connection.Close();

					return rowsAffected > 0;
				}
			}
			catch
			{
				throw;
			}
		}

		public DocumentPassport GetDocumentPassport(string documentID)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"SELECT DocumentID, UserID, PassportNumber, DateOfIssue, DateOfExpire
                             FROM PassportDocuments 
                             WHERE PassportNumber = @DocumentID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@DocumentID", documentID);

					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						DocumentPassport documentPassport = new DocumentPassport(
							(int)reader["DocumentID"],
							(int)reader["UserID"],
							(string)reader["PassportNumber"],
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfIssue"])),
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfExpire"]))
						);

						connection.Close();
						return documentPassport;
					}
					else
					{
						connection.Close();
						return null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public DocumentPassport GetDocumentPassportByUserID(int userID)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"SELECT DocumentID, UserID, PassportNumber, DateOfIssue, DateOfExpire
                             FROM PassportDocuments 
                             WHERE UserID = @UserID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@UserID", userID);

					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						DocumentPassport documentPassport = new DocumentPassport(
							(int)reader["DocumentID"],
							(int)reader["UserID"],
							(string)reader["PassportNumber"],
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfIssue"])),
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfExpire"]))
						);

						connection.Close();
						return documentPassport;
					}
					else
					{
						connection.Close();
						return null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public DocumentID GetDocumentID(string documentID)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"SELECT DocumentID, UserID, IDNumber, DateOfIssue, DateOfExpire
                             FROM IDDocuments 
                             WHERE IDNumber = @DocumentID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@DocumentID", documentID);

					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						DocumentID documentIDObj = new DocumentID(
							(int)reader["DocumentID"],
							(int)reader["UserID"],
							(string)reader["IDNumber"],
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfIssue"])),
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfExpire"]))
						);

						connection.Close();
						return documentIDObj;
					}
					else
					{
						connection.Close();
						return null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public DocumentID GetDocumentIDByUserID(int userID)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"SELECT DocumentID, UserID, IDNumber, DateOfIssue, DateOfExpire
                             FROM IDDocuments 
                             WHERE UserID = @UserID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@UserID", userID);

					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						DocumentID documentIDObj = new DocumentID(
							(int)reader["DocumentID"],
							(int)reader["UserID"],
							(string)reader["IDNumber"],
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfIssue"])),
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfExpire"]))
						);

						connection.Close();
						return documentIDObj;
					}
					else
					{
						connection.Close();
						return null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateDocumentID(DocumentID documentID)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"UPDATE IDDocuments 
                             SET UserID = @UserID, 
                                 IDNumber = @IDNumber, 
                                 DateOfIssue = @DateOfIssue, 
                                 DateOfExpire = @DateOfExpire
                             WHERE DocumentID = @DocumentID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@UserID", documentID.UserId);
					command.Parameters.AddWithValue("@IDNumber", documentID.IDNumber);
					command.Parameters.AddWithValue("@DateOfIssue", documentID.DateOfIssue);
					command.Parameters.AddWithValue("@DateOfExpire", documentID.DateOfExpire);
					command.Parameters.AddWithValue("@DocumentID", documentID.Id);

					int rowsAffected = command.ExecuteNonQuery();

					connection.Close();

					return rowsAffected > 0;
				}
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateDocumentPassport(DocumentPassport documentPassport)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"UPDATE PassportDocuments 
                             SET UserID = @UserID, 
                                 PassportNumber = @PassportNumber, 
                                 DateOfIssue = @DateOfIssue, 
                                 DateOfExpire = @DateOfExpire
                             WHERE DocumentID = @DocumentID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@UserID", documentPassport.UserId);
					command.Parameters.AddWithValue("@PassportNumber", documentPassport.PassportNumber);
					command.Parameters.AddWithValue("@DateOfIssue", documentPassport.DateOfIssue);
					command.Parameters.AddWithValue("@DateOfExpire", documentPassport.DateOfExpire);
					command.Parameters.AddWithValue("@DocumentID", documentPassport.Id);

					int rowsAffected = command.ExecuteNonQuery();

					connection.Close();

					return rowsAffected > 0;
				}
			}
			catch
			{
				throw;
			}
		}



		public bool DeleteDocumentPassport(int documentID)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"DELETE FROM PassportDocuments WHERE DocumentID = @DocumentID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@DocumentID", documentID);

					int rowsAffected = command.ExecuteNonQuery();

					connection.Close();

					return rowsAffected > 0;
				}
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteDocumentID(int id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"DELETE FROM IDDocuments WHERE DocumentID = @DocumentID";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@DocumentID", id);

					int rowsAffected = command.ExecuteNonQuery();

					connection.Close();

					return rowsAffected > 0;
				}
			}
			catch
			{
				throw;
			}
		}

		public List<DocumentPassport> GetAllPassports()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"SELECT DocumentID, UserID, PassportNumber, DateOfIssue, DateOfExpire FROM PassportDocuments";

					SqlCommand command = new SqlCommand(query, connection);
					SqlDataReader reader = command.ExecuteReader();

					List<DocumentPassport> passports = new List<DocumentPassport>();

					while (reader.Read())
					{
						DocumentPassport documentPassport = new DocumentPassport(
							(int)reader["DocumentID"],
							(int)reader["UserID"],
							(string)reader["PassportNumber"],
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfIssue"])),
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfExpire"]))
						);

						passports.Add(documentPassport);
					}

					connection.Close();
					return passports;
				}
			}
			catch
			{
				throw;
			}
		}

		public List<DocumentID> GetAllIDs()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string query = @"SELECT DocumentID, UserID, IDNumber, DateOfIssue, DateOfExpire FROM IDDocuments";

					SqlCommand command = new SqlCommand(query, connection);
					SqlDataReader reader = command.ExecuteReader();

					List<DocumentID> ids = new List<DocumentID>();

					while (reader.Read())
					{
						DocumentID documentID = new DocumentID(
							(int)reader["DocumentID"],
							(int)reader["UserID"],
							(string)reader["IDNumber"],
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfIssue"])),
							DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfExpire"]))
						);

						ids.Add(documentID);
					}

					connection.Close();
					return ids;
				}
			}
			catch
			{
				throw;
			}
		}
	}
}