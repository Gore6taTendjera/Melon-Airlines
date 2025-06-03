using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.DAL
{
	public interface IPassportDAL
	{
		bool CreateDocumentPassport(DocumentPassport documentPassport);

		List<DocumentPassport> GetAllPassports();
		DocumentPassport GetDocumentPassport(string passportNumber);
		DocumentPassport GetDocumentPassportByUserID(int userID);

		bool UpdateDocumentPassport(DocumentPassport documentPassport);

		bool DeleteDocumentPassport(int id);

	}
}
