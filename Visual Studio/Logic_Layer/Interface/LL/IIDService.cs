using DTOs;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.LL
{
    public interface IIDService
    {
        bool CreateID(int userId, string documentNumber, DateOnly dateOfIssue, DateOnly dateOfExpire);
        bool CreateIDDTO(DocumentDTO documentDTO);

        bool HasID(int userId);
		List<DocumentID> GetAllIDs();
		DocumentID GetIDByUserID(int userID);
        DocumentID GetID(string idNumber);
        DocumentDTO GetIDByUserIDDTO(int id);

        bool UpdateID(DocumentID documentID);

        bool DeleteID(int id);

	}
}
