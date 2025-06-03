using DTOs;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Shared_Classes;

namespace Logic_Layer.Services
{
	public class IDService : IIDService
	{
		private readonly IIDDAL _IDDAL;

		public IDService(IIDDAL iDDAL)
		{
			this._IDDAL = iDDAL;
		}

		// razor pages will force the user to write his profile details before creating document
		public bool CreateID(int userId, string documentNumber, DateOnly dateOfIssue, DateOnly dateOfExpire)
		{
			DocumentID documentID = new DocumentID(userId, documentNumber, dateOfIssue, dateOfExpire);
			if (_IDDAL.CreateDocumentID(documentID))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public bool CreateIDDTO(DocumentDTO IDDocument)
		{
			DateOnly? dateofissue = IDDocument.DateOfIssue.HasValue ? new DateOnly(IDDocument.DateOfIssue.Value.Year, IDDocument.DateOfIssue.Value.Month, IDDocument.DateOfIssue.Value.Day) : (DateOnly?)null;
			DateOnly? dateofexpire = IDDocument.DateOfExpire.HasValue ? new DateOnly(IDDocument.DateOfExpire.Value.Year, IDDocument.DateOfExpire.Value.Month, IDDocument.DateOfExpire.Value.Day) : (DateOnly?)null;


			DocumentID documentID = new(
				IDDocument.UserID,
				IDDocument.DocumentNumber,
				dateofissue,
				dateofexpire
			);


			return _IDDAL.CreateDocumentID(documentID);
		}


		public DocumentID GetIDByUserID(int userID)
		{
			DocumentID documentID = _IDDAL.GetDocumentIDByUserID(userID);
			return documentID;
		}

		public bool UpdateID(DocumentID documentID)
		{
			if (_IDDAL.UpdateDocumentID(documentID))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public DocumentID GetID(string idNumber)
		{
			return _IDDAL.GetDocumentID(idNumber);
		}

		public bool HasID(int userId)
		{
			DocumentID documentID = _IDDAL.GetDocumentIDByUserID(userId);
			if (documentID != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public DocumentDTO GetIDByUserIDDTO(int userId)
		{
			DocumentID documentID = _IDDAL.GetDocumentIDByUserID(userId);

			if (documentID == null)
			{
				return new DocumentDTO()
				{
					DocumentNumber = null,
					DateOfIssue = null,
					DateOfExpire = null
				};
			}

			return new DocumentDTO()
			{
				DocumentNumber = documentID.IDNumber,
				DateOfIssue = documentID.DateOfIssue.HasValue ? new DateTime(documentID.DateOfIssue.Value.Year, documentID.DateOfIssue.Value.Month, documentID.DateOfIssue.Value.Day) : (DateTime?)null,
				DateOfExpire = documentID.DateOfExpire.HasValue ? new DateTime(documentID.DateOfExpire.Value.Year, documentID.DateOfExpire.Value.Month, documentID.DateOfExpire.Value.Day) : (DateTime?)null
			};
		}

		public bool DeleteID(int id)
		{
			if (_IDDAL.DeleteDocumentID(id))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<DocumentID> GetAllIDs()
		{
			return _IDDAL.GetAllIDs();
		}

	}
}
