using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Classes;
using DTOs;

namespace Logic_Layer.Services
{
	public class ImageDBService : IImageService
	{
		private readonly IImageDAL _imageUploadDAL;

        public ImageDBService(IImageDAL imageUploadDAL)
        {
            _imageUploadDAL = imageUploadDAL;
        }

		public bool UploadImage(ImageDTO image)
		{
			Image img = new Image(image.UserId, image.Data, image.ContentType);

			return _imageUploadDAL.InsertImage(img);
		}

		public ImageDTO? GetImageByUserIDDTO(int userId)
		{
			Image image = _imageUploadDAL.GetImageByUserID(userId);

			if (image == null)
			{
				return null;
			}
			
			return new ImageDTO()
			{
				UserId = image.UserId,
				Data = image.Data,
				ContentType = image.ContentType
			}; 
		}

		public bool DeleteImageByUserID(int userId) 
		{
			return _imageUploadDAL.DeleteImageByUserID(userId);
		}

	}
}
