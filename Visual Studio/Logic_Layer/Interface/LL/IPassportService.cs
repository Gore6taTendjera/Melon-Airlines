using DTOs;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.LL
{
	public interface IPassportService
	{
		bool CreatePassport(int userId, string documentNumber, DateOnly dateOfIssue, DateOnly dateOfExpire);
		bool CreatePassportDTO(DocumentDTO documentPassport);

		bool HasPassport(int userId);
		List<DocumentPassport> GetAllPassports();
		DocumentPassport GetPassportByUserID(int userID);
		DocumentPassport GetPassport(string passportNumber);
		DocumentDTO GetPassportByUserIDDTO(int id);

		bool UpdatePassport(DocumentPassport documentPassport);
		bool UpdatePassportDTO(DocumentDTO documentPassport);

		bool DeletePassport(int id);
	}
}
