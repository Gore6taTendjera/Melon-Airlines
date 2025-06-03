using DTOs;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Shared_Classes;

namespace Logic_Layer
{
    public class PassportService : IPassportService
    {
        private readonly IPassportDAL _PassportDAL;

		public PassportService(IPassportDAL passportDAL)
        {
			this._PassportDAL = passportDAL;
        }


		public bool CreatePassport(int userId, string documentNumber, DateOnly dateOfIssue, DateOnly dateOfExpire)
        {
            DocumentPassport documentPassport = new DocumentPassport(userId, documentNumber, dateOfIssue, dateOfExpire);
			return _PassportDAL.CreateDocumentPassport(documentPassport);
        }

		public bool CreatePassportDTO(DocumentDTO documentPassport)
		{
			DateOnly? dateofissue = documentPassport.DateOfIssue.HasValue ? new DateOnly(documentPassport.DateOfIssue.Value.Year, documentPassport.DateOfIssue.Value.Month, documentPassport.DateOfIssue.Value.Day) : (DateOnly?)null;
			DateOnly? dateofexpire = documentPassport.DateOfExpire.HasValue ? new DateOnly(documentPassport.DateOfExpire.Value.Year, documentPassport.DateOfExpire.Value.Month, documentPassport.DateOfExpire.Value.Day) : (DateOnly?)null;

			DocumentPassport passport = new(
				documentPassport.UserID,
				documentPassport.DocumentNumber,
				dateofissue,
				dateofexpire
			);

			return _PassportDAL.CreateDocumentPassport(passport);
		}


		public DocumentPassport GetPassportByUserID(int userId)
		{
			DocumentPassport documentPassport = _PassportDAL.GetDocumentPassportByUserID(userId);
			return documentPassport;
		}

		public bool HasPassport(int userId)
		{
			DocumentPassport documentPassport = _PassportDAL.GetDocumentPassportByUserID(userId);
			if (documentPassport != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public DocumentPassport GetPassport(string passportNumber)
		{
			DocumentPassport documentPassport = _PassportDAL.GetDocumentPassport(passportNumber);
			return documentPassport;
		}

		public DocumentDTO GetPassportByUserIDDTO(int id)
		{
			DocumentPassport documentPassport = _PassportDAL.GetDocumentPassportByUserID(id);

			if (documentPassport == null)
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
				DocumentNumber = documentPassport.PassportNumber,
				DateOfIssue = documentPassport.DateOfIssue.HasValue ? new DateTime(documentPassport.DateOfIssue.Value.Year, documentPassport.DateOfIssue.Value.Month, documentPassport.DateOfIssue.Value.Day) : (DateTime?)null,
				DateOfExpire = documentPassport.DateOfExpire.HasValue ? new DateTime(documentPassport.DateOfExpire.Value.Year, documentPassport.DateOfExpire.Value.Month, documentPassport.DateOfExpire.Value.Day) : (DateTime?)null
			};
		}


		public bool UpdatePassport(DocumentPassport documentPassport)
		{
			return _PassportDAL.UpdateDocumentPassport(documentPassport);
		}

		public bool UpdatePassportDTO(DocumentDTO documentPassport)
		{
			DateOnly? dateofissue = documentPassport.DateOfIssue.HasValue ? new DateOnly(documentPassport.DateOfIssue.Value.Year, documentPassport.DateOfIssue.Value.Month, documentPassport.DateOfIssue.Value.Day) : (DateOnly?)null;
			DateOnly? dateofexpire = documentPassport.DateOfExpire.HasValue ? new DateOnly(documentPassport.DateOfExpire.Value.Year, documentPassport.DateOfExpire.Value.Month, documentPassport.DateOfExpire.Value.Day) : (DateOnly?)null;


			DocumentPassport passport = new(
				documentPassport.UserID,
				documentPassport.DocumentNumber,
				dateofissue,
				dateofexpire
			);


			return _PassportDAL.UpdateDocumentPassport(passport);
		}


		public bool DeletePassport(int id)
		{
			return _PassportDAL.DeleteDocumentPassport(id);
		}

		public List<DocumentPassport> GetAllPassports()
		{
			return _PassportDAL.GetAllPassports();
		}
	}
}
