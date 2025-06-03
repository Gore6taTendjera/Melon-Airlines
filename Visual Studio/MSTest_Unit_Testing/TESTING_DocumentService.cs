using System;
using System.Collections.Generic;
using Data_Access_Layer;
using DTOs;
using Logic_Layer;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared_Classes;

namespace uMSTest_Unit_Testing
{
    [TestClass]
    public class TESTING_DocumentService
    {
        private FAKE_DocumentDAL _fakeDocumentDAL;
        private IDService _idService;
        private PassportService _passportService;

        [TestInitialize]
        public void Setup()
        {
            _fakeDocumentDAL = new FAKE_DocumentDAL();
            _idService = new IDService(_fakeDocumentDAL);
            _passportService = new PassportService(_fakeDocumentDAL);
        }

        [TestMethod]
        public void IDService_CreateID()
        {
            // Arrange
            int userId = 6;
            string documentNumber = "ID006";
            DateOnly dateOfIssue = new DateOnly(2023, 6, 1);
            DateOnly dateOfExpire = new DateOnly(2024, 6, 1);

            // Act
            bool result = _idService.CreateID(userId, documentNumber, dateOfIssue, dateOfExpire);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IDService_GetIDByUserID()
        {
            // Arrange
            int userId = 1;

            // Act
            DocumentID documentID = _idService.GetIDByUserID(userId);

            // Assert
            Assert.IsNotNull(documentID);
            Assert.AreEqual(userId, documentID.UserId);
        }

        [TestMethod]
        public void IDService_UpdateID()
        {
            // Arrange
            var documentID = new DocumentID(1, 1, "ID001", new DateOnly(2022, 1, 1), new DateOnly(2023, 1, 1));

            // Act
            bool result = _idService.UpdateID(documentID);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IDService_GetID()
        {
            // Arrange
            string idNumber = "ID001";

            // Act
            DocumentID documentID = _idService.GetID(idNumber);

            // Assert
            Assert.IsNotNull(documentID);
            Assert.AreEqual(idNumber, documentID.IDNumber);
        }

        [TestMethod]
        public void IDService_GetIDByUserIDDTO()
        {
            // Arrange
            int userId = 1;

            // Act
            DocumentDTO documentDTO = _idService.GetIDByUserIDDTO(userId);

            // Assert
            Assert.IsNotNull(documentDTO);
            Assert.AreEqual("ID001", documentDTO.DocumentNumber);
        }

        [TestMethod]
        public void IDService_DeleteID()
        {
            // Arrange
            int id = 1;

            // Act
            bool result = _idService.DeleteID(id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IDService_GetAllIDs()
        {
            // Act
            List<DocumentID> documentIDs = _idService.GetAllIDs();

            // Assert
            Assert.IsNotNull(documentIDs);
            Assert.AreEqual(5, documentIDs.Count);
        }

        [TestMethod]
        public void PassportService_CreatePassport()
        {
            // Arrange
            int userId = 6;
            string documentNumber = "PP006";
            DateOnly dateOfIssue = new DateOnly(2023, 6, 1);
            DateOnly dateOfExpire = new DateOnly(2024, 6, 1);

            // Act
            bool result = _passportService.CreatePassport(userId, documentNumber, dateOfIssue, dateOfExpire);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PassportService_GetPassportByUserID()
        {
            // Arrange
            int userId = 1;

            // Act
            DocumentPassport documentPassport = _passportService.GetPassportByUserID(userId);

            // Assert
            Assert.IsNotNull(documentPassport);
            Assert.AreEqual(userId, documentPassport.UserId);
        }

        [TestMethod]
        public void PassportService_GetPassport()
        {
            // Arrange
            string passportNumber = "PP001";

            // Act
            DocumentPassport documentPassport = _passportService.GetPassport(passportNumber);

            // Assert
            Assert.IsNotNull(documentPassport);
            Assert.AreEqual(passportNumber, documentPassport.PassportNumber);
        }

        [TestMethod]
        public void PassportService_GetPassportByUserIDDTO()
        {
            // Arrange
            int userId = 1;

            // Act
            DocumentDTO documentDTO = _passportService.GetPassportByUserIDDTO(userId);

            // Assert
            Assert.IsNotNull(documentDTO);
            Assert.AreEqual("PP001", documentDTO.DocumentNumber);
        }

        [TestMethod]
        public void PassportService_UpdatePassport()
        {
            // Arrange
            var documentPassport = new DocumentPassport(1, 1, "PP001", new DateOnly(2022, 1, 1), new DateOnly(2023, 1, 1));

            // Act
            bool result = _passportService.UpdatePassport(documentPassport);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PassportService_DeletePassport()
        {
            // Arrange
            int id = 1;

            // Act
            bool result = _passportService.DeletePassport(id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PassportService_GetAllPassports()
        {
            // Act
            List<DocumentPassport> documentPassports = _passportService.GetAllPassports();

            // Assert
            Assert.IsNotNull(documentPassports);
            Assert.AreEqual(5, documentPassports.Count);
        }
    }
}
