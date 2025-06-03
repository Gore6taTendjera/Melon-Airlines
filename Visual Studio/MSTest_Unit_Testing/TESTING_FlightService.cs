using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Logic_Layer;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic_Layer.Services.Planes;
using Enums;
using DTOs;

namespace uMSTest_Unit_Testing
{
    [TestClass]
    public class TESTING_FlightService
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
        public void CreateFlight_ReturnsTrue()
        {
            // Arrange
            Airport origin = new Airport(1, "AAA", "Origin Airport", "City", "Country", "Timezone");
            Airport destination = new Airport(2, "BBB", "Destination Airport", "City", "Country", "Timezone");
            DateTime takeoff = DateTime.Now;
            DateTime arrival = takeoff.AddHours(2);
            double price = 1000.0;
            Plane plane = new A320(1, "REG123", PlaneModel.A320, 150, origin, PlaneStatus.InService);
            FlightStatus flightStatus = FlightStatus.DELAYED;

            // Act
            bool result = _flightService.CreateFlight(origin, destination, takeoff, arrival, price, plane, flightStatus);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteFlightByID_ReturnsTrue()
        {
            // Arrange
            int id = 1;

            // Act
            bool result = _flightService.DeleteFlightByID(id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllFlights_ReturnsCorrectFlights()
        {
            // Act
            var flights = _flightService.GetAllFlights();

            // Assert
            Assert.IsNotNull(flights);
        }

        [TestMethod]
        public void GetFlightByID_ReturnsFlight()
        {
            // Arrange
            int flightId = 1;

            // Act
            var flight = _flightService.GetFlightByID(flightId);

            // Assert
            Assert.IsNotNull(flight);
            Assert.AreEqual(flightId, flight.FlightID);
        }

        [TestMethod]
        public void GetFlightPrice_ReturnsCorrectPrice()
        {
            // Arrange
            int flightID = 1;
            SeatModel seatModel = SeatModel.Economy;

            // Act
            var price = _flightService.GetFlightPrice(flightID, seatModel);

            // Assert
            Assert.IsNotNull(price);
            Assert.AreEqual(1000.0, price);
        }

        [TestMethod]
        public void UpdateFlight_ReturnsTrue()
        {
            // Arrange
            var flight = new Flight(1, new Airport(1, "AAA", "Origin Airport", "City", "Country", "Timezone"), new Airport(2, "BBB", "Destination Airport", "City", "Country", "Timezone"), DateTime.Now, DateTime.Now.AddHours(2), 1000.0, new A380(1, "REG123", PlaneModel.A320, 150, new Airport(1, "AAA", "Origin Airport", "City", "Country", "Timezone"), PlaneStatus.InService), FlightStatus.LANDED);

            // Act
            bool result = _flightService.UpdateFlight(flight);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetFlightByIdDTO_ReturnsCorrectDTO()
        {
            // Arrange
            int flightId = 1;

            // Act
            FlightDTO flightDTO = _flightService.GetFlightByIdDTO(flightId);

            // Assert
            Assert.IsNotNull(flightDTO);
            Assert.AreEqual(flightId, flightDTO.FlightId);
        }


        [TestMethod]
        public void GetAllFlightsByLocationTimeDate_OneWay()
        {
            // Arrange
            string originCity = "Dubai";
            string destinationCity = "Tokyo";
            DateTime departureDate = DateTime.Now.Date;

            // Act
            var flights = _flightService.GetAllFlightsByLocationTimeDate(originCity, destinationCity, departureDate);

            // Assert
            Assert.AreEqual(1, flights.Count);
            Assert.AreEqual(1, flights[0].FlightID);
        }

        [TestMethod]
        public void GetAllFlightsByLocationTimeDate_RoundTrip()
        {
            // Arrange
            string originCity = "Dubai";
            string destinationCity = "Tokyo";
            DateTime departureDate = DateTime.Now.Date;
            DateTime returnDate = DateTime.Now.Date.AddDays(1);

            // Act
            var flights = _flightService.GetAllFlightsByLocationTimeDate(originCity, destinationCity, departureDate, returnDate);

            // Assert
            Assert.AreEqual(2, flights.Count);
            Assert.AreEqual(1, flights[0].FlightID);
            Assert.AreEqual(2, flights[1].FlightID);
        }

        [TestMethod]
        public void Test_GetAllFlightsByLocationTimeDateDTO_RoundTrip()
        {
            // Arrange
            string originCity = "Dubai";
            string destinationCity = "Tokyo";
            DateTime departureDate = DateTime.Now.Date;
            DateTime returnDate = DateTime.Now.Date.AddDays(1);

            // Act
            var flightDTOs = _flightService.GetAllFlightsByLocationTimeDateDTO(originCity, destinationCity, departureDate, returnDate);

            // Assert
            Assert.AreEqual(2, flightDTOs.Count);
            Assert.AreEqual(1, flightDTOs[0].FlightId);
            Assert.AreEqual("Dubai International", flightDTOs[0].DepartureAirport.AirportName);
            Assert.AreEqual("Narrita Airport", flightDTOs[0].ArrivalAirport.AirportName);
            Assert.AreEqual(2, flightDTOs[1].FlightId);
            Assert.AreEqual("Narrita Airport", flightDTOs[1].DepartureAirport.AirportName);
            Assert.AreEqual("Dubai International", flightDTOs[1].ArrivalAirport.AirportName);
        }
    }
}
