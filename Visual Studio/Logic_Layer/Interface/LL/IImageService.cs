using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.LL
{
	public interface IImageService
	{
		bool UploadImage(ImageDTO image);
		ImageDTO GetImageByUserIDDTO(int userId);
		bool DeleteImageByUserID(int userId);
	}
}
