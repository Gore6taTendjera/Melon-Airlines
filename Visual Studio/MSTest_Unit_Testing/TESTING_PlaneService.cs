using Logic_Layer.Interface.DAL;
using Logic_Layer;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic_Layer.Services.Planes;

namespace uMSTest_Unit_Testing
{
    [TestClass]
    public class TESTING_PlaneService
    {
        private TicketService _ticketService;
        private FlightService _flightService;
        private PlaneService _planeService;
        private PlaneSeatsService _planeSeatsService;
        private PlaneSeatsServiceFactory _planeSeatsServiceFactory;
        private SeatService _seatService;

        private ITicketsDAL _fakeTicketDAL;
        private IFlightDAL _fakeFlightDAL;
        private IPlaneDAL _fakePlaneDAL;

        [TestInitialize]
        public void Setup()
        {
            // dependencies
            _fakeTicketDAL = new FAKE_TicketsDAL();
            _fakeFlightDAL = new FAKE_FlightsDAL();
            _fakePlaneDAL = new FAKE_PlaneDAL();

            _planeSeatsServiceFactory = new PlaneSeatsServiceFactory();
            _ticketService = new TicketService(_fakeTicketDAL);
            _planeService = new PlaneService(_fakePlaneDAL, _ticketService, _planeSeatsServiceFactory);
            _flightService = new FlightService(_fakeFlightDAL);

        }

        [TestMethod]
        public void CreateNewPlane_ReturnsTrue()
        {
            // Arrange
            Plane plane = new A380(99, "NEWPLANE", PlaneModel.A320, 250, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London"), PlaneStatus.InService);

            // Act
            bool result = _fakePlaneDAL.CreateNewPlane(plane);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeletePlaneByID_ReturnsTrue()
        {
            // Arrange
            int planeId = 1;

            // Act
            bool result = _fakePlaneDAL.DeletePlaneByID(planeId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetPlaneByFlightID_ReturnsCorrectPlane()
        {
            // Arrange
            int flightID = 1;

            // Act
            var plane = _fakePlaneDAL.GetPlaneByFlightID(flightID);

            // Assert
            Assert.IsNotNull(plane);
            Assert.AreEqual(flightID, plane.ID);
        }

        [TestMethod]
        public void GetPlaneByFlightID_ReturnsNoPlane()
        {
            // Arrange
            int flightID = 378954;

            // Act
            var plane = _fakePlaneDAL.GetPlaneByFlightID(flightID);

            // Assert
            Assert.IsNull(plane);
        }

        [TestMethod]
        public void GetPlaneByID_ReturnsCorrectPlane()
        {
            // Arrange
            int id = 1;

            // Act
            var plane = _fakePlaneDAL.GetPlaneByID(id);

            // Assert
            Assert.IsNotNull(plane);
            Assert.AreEqual(id, plane.ID);
        }

        [TestMethod]
        public void GetPlaneByID_ReturnsNoPlane()
        {
            // Arrange
            int id = 62345;

            // Act
            var plane = _fakePlaneDAL.GetPlaneByID(id);

            // Assert
            Assert.IsNull(plane);
        }

        [TestMethod]
        public void UpdatePlane_ReturnsTrue()
        {
            // Arrange
            Plane plane = new A320(1, "VFNKL", PlaneModel.A320, 200, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London"), PlaneStatus.InService);
            Plane updatedPlane = new A380(1, "VFNKL", PlaneModel.A320, 210, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London"), PlaneStatus.InService);

            // Act
            bool result = _fakePlaneDAL.UpdatePlane(updatedPlane);

            // Assert
            Assert.IsTrue(result);
        } 
        
        [TestMethod]
        public void UpdatePlane_ReturnsFalse()
        {
            // Arrange
            Plane updatedPlane = new A380(8742, "VFNKL", PlaneModel.A320, 210, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London"), PlaneStatus.InService);

            // Act
            bool result = _fakePlaneDAL.UpdatePlane(updatedPlane);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
