using Data_Access_Layer;
using DTOs;
using Enums;
using Logic_Layer;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services.Planes;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uMSTest_Unit_Testing
{
    [TestClass]
    public class TESTING_SeatService
    {
        private TicketService _ticketService;
        private PlaneService _planeService;
        private PlaneSeatsServiceFactory _planeSeatsServiceFactory;
        private SeatService _seatService;

        private ITicketsDAL _fakeTicketDAL;
        private IPlaneDAL _fakePlaneDAL;


        [TestInitialize]
        public void Setup()
        {
            _fakeTicketDAL = new FAKE_TicketsDAL();
            _fakePlaneDAL = new FAKE_PlaneDAL();

            _planeSeatsServiceFactory = new PlaneSeatsServiceFactory();
            _ticketService = new TicketService(_fakeTicketDAL);
            _planeService = new PlaneService(_fakePlaneDAL, _ticketService, _planeSeatsServiceFactory);
            _seatService = new SeatService(_planeService, _ticketService);
        }


        [TestMethod]
        public void GetAvailableSeatsByFlightID_ShouldReturnAvailableSeats()
        {
            // Arrange
            int flightID = 1; // A320

            // Act
            List<Seat> availableSeats = _seatService.GetAvailableSeatsByFlightID(flightID);

            // Assert
            Assert.IsNotNull(availableSeats);
            Assert.IsTrue(availableSeats.Count > 0);
        }

        [TestMethod]
        public void GetAllSeatsByFlightID_ShouldReturnAllSeatsA320()
        {
            // Arrange
            int flightID = 1; // A320

            // Act
            List<Seat> allSeats = _seatService.GetAllSeatsByFlightID(flightID);

            // Assert
            Assert.IsNotNull(allSeats);
            Assert.IsTrue(allSeats.Count > 0);
        }

        [TestMethod]
        public void GetAllSeatsByFlightID_ShouldReturnAllSeatsA380()
        {
            // Arrange
            int flightID = 2; // A380

            // Act
            List<Seat> allSeats = _seatService.GetAllSeatsByFlightID(flightID);

            // Assert
            Assert.IsNotNull(allSeats);
            Assert.IsTrue(allSeats.Count > 0);
        }

        [TestMethod]
        public void GetAvailableFirstSeatsByFlightID_ShouldReturnAvailableFirstClassSeatsA380()
        {
            // Arrange
            int flightID = 2; // A380

            // Act
            List<Seat> availableFirstSeats = _seatService.GetAvailableFirstSeatsByFlightID(flightID);

            // Assert
            Assert.IsNotNull(availableFirstSeats);
            Assert.IsTrue(availableFirstSeats.Count > 0);
            Assert.IsTrue(availableFirstSeats.All(seat => seat.SeatModel == SeatModel.First));
        }

        [TestMethod]
        public void GetAvailableBusinessSeatsByFlightID_ShouldReturnAvailableBusinessClassSeatsA380()
        {
            // Arrange
            int flightID = 2; // A380

            // Act
            List<Seat> availableBusinessSeats = _seatService.GetAvailableBusinessSeatsByFlightID(flightID);

            // Assert
            Assert.IsNotNull(availableBusinessSeats);
            Assert.IsTrue(availableBusinessSeats.Count > 0);
            Assert.IsTrue(availableBusinessSeats.All(seat => seat.SeatModel == SeatModel.Business));
        }

        [TestMethod]
        public void GetAvailableEconomySeatsByFlightID_ShouldReturnAvailableEconomyClassSeatsA380()
        {
            // Arrange
            int flightID = 1; // A320

            // Act
            List<Seat> availableEconomySeats = _seatService.GetAvailableEconomySeatsByFlightID(flightID);

            // Assert
            Assert.IsNotNull(availableEconomySeats);
            Assert.IsTrue(availableEconomySeats.Count > 0);
            Assert.IsTrue(availableEconomySeats.All(seat => seat.SeatModel == SeatModel.Economy));
        }

        [TestMethod]
        public void GetAvailableEconomySeatsByFlightID_ShouldReturnAvailableEconomyClassSeatsA320()
        {
            // Arrange
            int flightID = 1; // A320

            // Act
            List<Seat> availableEconomySeats = _seatService.GetAvailableEconomySeatsByFlightID(flightID);

            // Assert
            Assert.IsNotNull(availableEconomySeats);
            Assert.IsTrue(availableEconomySeats.Count > 0);
            Assert.IsTrue(availableEconomySeats.All(seat => seat.SeatModel == SeatModel.Economy));
        }


        [TestMethod]
        public void GetAvailableFirstSeatByFlightID_ShouldReturnSeatA380()
        {
            // Arrange
            int flightID = 2; // A320

            // Act
            Seat seat = _seatService.GetAvailableFirstSeatByFlightID(flightID);
            // Assert
            Assert.IsNotNull(seat);
            Assert.IsTrue(seat.SeatModel == SeatModel.First);
        }

        [TestMethod]
        public void GetAvailableBusinessSeatByFlightID_ShouldReturnSeatA380()
        {
            // Arrange
            int flightID = 2; // A320

            // Act
            Seat seat = _seatService.GetAvailableBusinessSeatByFlightID(flightID);
            // Assert
            Assert.IsNotNull(seat);
            Assert.IsTrue(seat.SeatModel == SeatModel.Business);
        }

        [TestMethod]
        public void GetAvailableEconomySeatByFlightID_ShouldReturnSeatA380()
        {
            // Arrange
            int flightID = 2; // A320

            // Act
            Seat seat = _seatService.GetAvailableEconomySeatByFlightID(flightID);
            // Assert
            Assert.IsNotNull(seat);
            Assert.IsTrue(seat.SeatModel == SeatModel.Economy);
        }

        [TestMethod]
        public void GetAvailableEconomySeatByFlightID_ShouldReturnSeatA320()
        {
            // Arrange
            int flightID = 1; // A320

            // Act
            Seat seat = _seatService.GetAvailableEconomySeatByFlightID(flightID);
            // Assert
            Assert.IsNotNull(seat);
            Assert.IsTrue(seat.SeatModel == SeatModel.Economy);
        }



    }
}
