using Logic_Layer.Interface.DAL;
using Logic_Layer;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uMSTest_Unit_Testing
{
    [TestClass]
    public class TESTING_AirportService
    {
        private IAirportDAL _fakeAirportDAL;

        [TestInitialize]
        public void Setup()
        {
            _fakeAirportDAL = new FAKE_AirportDAL();
        }

        [TestMethod]
        public void CreateAirport_ReturnsTrue()
        {
            // Arrange
            Airport airport = new Airport(3, "LAX", "Los Angeles International", "Los Angeles", "USA", "USA/Pacific");

            // Act
            bool result = _fakeAirportDAL.CreateAirport(airport);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAirportByID_ReturnsCorrectAirport()
        {
            // Arrange
            int id = 1;

            // Act
            var airport = _fakeAirportDAL.GetAirportByID(id);

            // Assert
            Assert.IsNotNull(airport);
            Assert.AreEqual(id, airport.ID);
        }

        [TestMethod]
        public void DeleteAirportByID_ReturnsTrue()
        {
            // Arrange
            int id = 1;

            // Act
            bool result = _fakeAirportDAL.DeleteAirportByID(id);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
