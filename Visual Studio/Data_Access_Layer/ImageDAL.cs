using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Classes;
using Logic_Layer.Interface.DAL;

namespace Data_Access_Layer
{
	public class ImageDAL : Base, IImageDAL
	{

		public bool InsertImage(Image image)
		{
			try
			{
				using (MemoryStream ms = new MemoryStream())
				{
					ms.Write(image.Data, 0, image.Data.Length);
					ms.Position = 0;

					using (var connection = new SqlConnection(connectionString))
					{
						connection.Open();
						using (var command = new SqlCommand())
						{
							command.Connection = connection;

							command.CommandText = "SELECT COUNT(1) FROM Images WHERE UserId = @UserId";
							command.Parameters.AddWithValue("@UserId", image.UserId);

							bool imageExists = (int)command.ExecuteScalar() > 0;

							if (imageExists)
							{
								command.CommandText = "UPDATE Images SET Data = @Data, ContentType = @ContentType WHERE UserId = @UserId";
							}
							else
							{
								command.CommandText = "INSERT INTO Images (UserId, Data, ContentType) VALUES (@UserId, @Data, @ContentType)";
							}

							command.Parameters.Clear();
							command.Parameters.AddWithValue("@UserId", image.UserId);
							command.Parameters.AddWithValue("@Data", ms.ToArray());
							command.Parameters.AddWithValue("@ContentType", image.ContentType);

							return command.ExecuteNonQuery() > 0;
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public Image GetImageByUserID(int userId)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					using (var command = new SqlCommand())
					{
						command.Connection = connection;
						command.CommandText = "SELECT Id, Data, ContentType FROM Images WHERE UserId = @UserId";
						command.Parameters.AddWithValue("@UserId", userId);

						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								int id = (int)reader["Id"];
								byte[] data = (byte[])reader["Data"];
								string contentType = reader["ContentType"].ToString();

								return new Image(id, userId, data, contentType);
							}
							else
							{
								return null;
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteImageByUserID(int userId)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					using (var command = new SqlCommand())
					{
						command.Connection = connection;
						command.CommandText = "DELETE FROM Images WHERE UserId = @UserId";
						command.Parameters.AddWithValue("@UserId", userId);

						return command.ExecuteNonQuery() > 0;
					}
				}
			}
			catch
			{
				throw;
			}
		}



	}
}
