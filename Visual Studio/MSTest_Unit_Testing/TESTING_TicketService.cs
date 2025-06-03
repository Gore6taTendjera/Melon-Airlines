using System.Linq;
using Data_Access_Layer;
using Logic_Layer;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shared_Classes;
using System.Collections.Generic;
using Logic_Layer.Services.Planes;
using Enums;

namespace uMSTest_Unit_Testing
{
    [TestClass]
    public class TESTING_TicketService
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

            _seatService = new SeatService(_planeService, _ticketService);
            _planeSeatsServiceFactory = new PlaneSeatsServiceFactory();
            _ticketService = new TicketService(_fakeTicketDAL);
            _planeService = new PlaneService(_fakePlaneDAL, _ticketService, _planeSeatsServiceFactory);
            _flightService = new FlightService(_fakeFlightDAL);

        }

        [TestMethod]
        public void GetAllTickets_ReturnsCorrectTickets()
        {
            // Act
            var tickets = _ticketService.GetAllTickets();

            // Assert
            Assert.IsNotNull(tickets);
        }

        [TestMethod]
        public void CreateTicket_WithoutSeat_Success()
        {
            // Arrange
            int flightID = 1;
            int userID = 1;

            // Act
            bool result = _ticketService.CreateTicket(flightID, userID);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateTicket_WithSeat_Success()
        {
            // Arrange
            int flightID = 1;
            int userID = 1;
            SeatModel seatModel = SeatModel.Economy;
            int seatRow = 10;
            char seatColumn = 'A';

            // Act
            bool result = _ticketService.CreateTicket(flightID, userID, seatModel, seatRow, seatColumn);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateTicket_ReturnsTrue()
        {
            // Arrange
            var ticket = new Ticket(1, 123, 101, SeatModel.Economy, 20, 'B');

            // Act
            var result = _fakeTicketDAL.UpdateTicket(ticket);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateTicket_ReturnsFalse()
        {
            // Arrange
            var ticket = new Ticket(435, 00123, 1101, SeatModel.Economy, 220, 'Z');

            // Act
            var result = _fakeTicketDAL.UpdateTicket(ticket);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllTickets_Success()
        {
            // Act
            var result = _ticketService.GetAllTickets();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTicketByID_ReturnsTicket()
        {
            // Arrange
            int ticketId = 1;

            // Act
            var ticket = _fakeTicketDAL.GetTicketByID(ticketId);

            // Assert
            Assert.IsNotNull(ticket);
            Assert.AreEqual(ticketId, ticket.ID);
        }    
        
        [TestMethod]
        public void GetTicketByID_ReturnsNull()
        {
            // Arrange
            int ticketId = 3333331;

            // Act
            var ticket = _fakeTicketDAL.GetTicketByID(ticketId);

            // Assert
            Assert.IsNull(ticket);
        }

        [TestMethod]
        public void GetAllTicketsByPassengerID_ReturnsTickets()
        {
            // Arrange
            int passengerId = 123;

            // Act
            var tickets = _fakeTicketDAL.GetAllTicketsByPassengerID(passengerId);

            // Assert
            Assert.IsNotNull(tickets);
            Assert.IsTrue(tickets.Any());
        }

        [TestMethod]
        public void GetAllTicketsByFlightID_ReturnsTickets()
        {
            // Arrange
            int flightId = 101;

            // Act
            var tickets = _fakeTicketDAL.GetAllTicketsByFlightID(flightId);

            // Assert
            Assert.IsNotNull(tickets);
            Assert.IsTrue(tickets.Any());
        }

        [TestMethod]
        public void DeleteTicketByID_ReturnsTrue()
        {
            // Arrange
            int ticketId = 1;

            // Act
            var result = _fakeTicketDAL.DeleteTicketByID(ticketId);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void DeleteTicketByID_ReturnsFalse()
        {
            // Arrange
            int ticketId = 9999;

            // Act
            var result = _fakeTicketDAL.DeleteTicketByID(ticketId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
