using System;

namespace Shared_Classes
{
	public class Image
	{
		public int Id { get; private set; }
		public int UserId { get; private set; }
		public byte[] Data { get; private set; }
		public string ContentType { get; private set; }

		public Image(int userId, byte[] data, string contentType)
		{
			if (userId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
			}

			if (data == null || data.Length == 0)
			{
				throw new ArgumentException("Image data cannot be null or empty.", nameof(data));
			}

			if (string.IsNullOrWhiteSpace(contentType))
			{
				throw new ArgumentException("Content type cannot be null or empty.", nameof(contentType));
			}

			UserId = userId;
			Data = data;
			ContentType = contentType;
		}

		public Image(int id, int userId, byte[] data, string contentType)
			: this(userId, data, contentType)
		{
			Id = id;
		}
	}
}