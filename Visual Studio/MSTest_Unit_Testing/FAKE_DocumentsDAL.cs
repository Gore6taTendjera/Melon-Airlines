using System;
using System.Collections.Generic;
using Shared_Classes;
using Logic_Layer.Interface.DAL;

namespace Data_Access_Layer
{
    // fake repository
    public class FAKE_DocumentDAL : IIDDAL, IPassportDAL
    {
        Dictionary<int, DocumentID> _idDocuments = new Dictionary<int, DocumentID>
        {
            { 1, new DocumentID(1, 1, "ID001", new DateOnly(2022, 1, 1), new DateOnly(2023, 1, 1)) },
            { 2, new DocumentID(2, 2, "ID002", new DateOnly(2023, 2, 1), new DateOnly(2024, 2, 1)) },
            { 3, new DocumentID(3, 3, "ID003", new DateOnly(2024, 3, 1), new DateOnly(2025, 3, 1)) },
            { 4, new DocumentID(4, 4, "ID004", new DateOnly(2021, 4, 1), new DateOnly(2022, 4, 1)) },
            { 5, new DocumentID(5, 5, "ID005", new DateOnly(2022, 5, 1), new DateOnly(2023, 5, 1)) }
        };

        Dictionary<int, DocumentPassport> _passports = new Dictionary<int, DocumentPassport>
        {
            { 1, new DocumentPassport(1, 1, "PP001", new DateOnly(2022, 1, 1), new DateOnly(2023, 1, 1)) },
            { 2, new DocumentPassport(2, 2, "PP002", new DateOnly(2023, 2, 1), new DateOnly(2024, 2, 1)) },
            { 3, new DocumentPassport(3, 3, "PP003", new DateOnly(2024, 3, 1), new DateOnly(2025, 3, 1)) },
            { 4, new DocumentPassport(4, 4, "PP004", new DateOnly(2021, 4, 1), new DateOnly(2022, 4, 1)) },
            { 5, new DocumentPassport(5, 5, "PP005", new DateOnly(2022, 5, 1), new DateOnly(2023, 5, 1)) }
        };

        public bool CreateDocumentID(DocumentID documentID)
        {
            if (_idDocuments.ContainsKey(documentID.Id))
                return false; // already exists

            _idDocuments.Add(documentID.Id, documentID);
            return true;
        }

        public bool CreateDocumentPassport(DocumentPassport documentPassport)
        {
            if (_passports.ContainsKey(documentPassport.Id))
                return false;

            _passports.Add(documentPassport.Id, documentPassport);
            return true;
        }

        public bool DeleteDocumentID(int id)
        {
            return _idDocuments.Remove(id);
        }

        public bool DeleteDocumentPassport(int id)
        {
            return _passports.Remove(id);
        }

        public List<DocumentID> GetAllIDs()
        {
            return new List<DocumentID>(_idDocuments.Values);
        }

        public List<DocumentPassport> GetAllPassports()
        {
            return new List<DocumentPassport>(_passports.Values);
        }

        public DocumentID GetDocumentID(string idNumber)
        {
            foreach (var documentID in _idDocuments.Values)
            {
                if (documentID.IDNumber == idNumber)
                    return documentID;
            }
            return null;
        }

        public DocumentID GetDocumentIDByUserID(int userID)
        {
            foreach (var documentID in _idDocuments.Values)
            {
                if (documentID.UserId == userID)
                    return documentID;
            }
            return null;
        }

        public DocumentPassport GetDocumentPassport(string passportNumber)
        {
            foreach (var passport in _passports.Values)
            {
                if (passport.PassportNumber == passportNumber)
                    return passport;
            }
            return null;
        }

        public DocumentPassport GetDocumentPassportByUserID(int userID)
        {
            foreach (var passport in _passports.Values)
            {
                if (passport.UserId == userID)
                    return passport;
            }
            return null;
        }

        public bool UpdateDocumentID(DocumentID documentID)
        {
            if (_idDocuments.ContainsKey(documentID.Id))
            {
                _idDocuments[documentID.Id] = documentID;
                return true;
            }
            return false;
        }

        public bool UpdateDocumentPassport(DocumentPassport documentPassport)
        {
            if (_passports.ContainsKey(documentPassport.Id))
            {
                _passports[documentPassport.Id] = documentPassport;
                return true;
            }
            return false;
        }
    }
}