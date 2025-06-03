using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data_Access_Layer;
using Logic_Layer;
using Shared_Classes;
using System.Collections.Generic;
using Enums;
using DTOs;

namespace uMSTest_Unit_Testing
{
    [TestClass]
    public class TESTING_UserAccountService
    {
        private FAKE_UserAccountDAL _fakeUserAccountDAL;
        private UserAccountService _userAccountService;

        [TestInitialize]
        public void Setup()
        {
            _fakeUserAccountDAL = new FAKE_UserAccountDAL();
            _userAccountService = new UserAccountService(_fakeUserAccountDAL);
        }


        [TestMethod]
        public void CreateUser_ValidUser_ReturnsTrue()
        {
            // Arrange
            User user = new User("TestUser", "test123", "test@example.com");

            // Act
            bool result = _userAccountService.CreateUser(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Register_ValidUser_ReturnsTrue()
        {
            // Arrange
            string username = "TestUser";
            string password = "test123";
            string email = "test@example.com";

            // Act
            bool result = _userAccountService.Register(username, password, email);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Act
            List<User> users = _userAccountService.GetAllUsers();

            // Assert
            Assert.IsNotNull(users);
        }

        [TestMethod]
        public void GetUserByID_ExistingID_ReturnsUser()
        {
            // Arrange
            int userId = 1;

            // Act
            User user = _userAccountService.GetUserByID(userId);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.ID);
        }

        [TestMethod]
        public void Authenticate_ValidCredentials_ReturnsUserLoginDTO()
        {
            // Arrange
            string username = "v7";
            string password = "v7";

            // Act
            UserLoginDTO userLoginDTO = _userAccountService.Authenticate(username, password);

            // Assert
            Assert.IsNotNull(userLoginDTO);
            Assert.AreEqual(username, userLoginDTO.Username);
        }

        [TestMethod]
        public void GetUserProfileDetails_ExistingID_ReturnsUserProfileDetailsDTO()
        {
            // Arrange
            int userId = 1;

            // Act
            UserProfileDetailsDTO userProfile = _userAccountService.GetUserProfileDetails(userId);

            // Assert
            Assert.IsNotNull(userProfile);
        }

        [TestMethod]
        public void UpdateUser_ValidUser_ReturnsTrue()
        {
            // Arrange
            User user = new User(1, "John2", "Doe2", "Smith", Gender.Male, new DateOnly(1990, 1, 1), 
                "New York", "john_doe", "password123", "john@example.com", Nationality.American, UserType.Normal);

            // Act
            bool result = _userAccountService.UpdateUser(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUser_ExistingID_ReturnsTrue()
        {
            // Arrange
            int userId = 1;

            // Act
            bool result = _userAccountService.DeleteUser(userId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}