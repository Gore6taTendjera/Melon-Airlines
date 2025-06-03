using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Classes;

namespace Logic_Layer.Interface.DAL
{
    public interface IIDDAL
    {
        bool CreateDocumentID(DocumentID documentID);

		List<DocumentID> GetAllIDs();
		DocumentID GetDocumentID(string idNumber);
		DocumentID GetDocumentIDByUserID(int userID);

		bool UpdateDocumentID(DocumentID documentID);

		bool DeleteDocumentID(int id);

	}
}
