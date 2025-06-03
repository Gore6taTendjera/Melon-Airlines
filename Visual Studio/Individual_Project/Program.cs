using Logic_Layer;
using System;
using Data_Access_Layer;
using Shared_Classes;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services;
using Logic_Layer.Services.Planes;

namespace Class_Library
{
    class Program
    {
        static void Main(string[] args)
        {

            //Database db = new Database();



            //DocumentService documentService = new DocumentService();
            //documentService.CreatePassport(user, "ABC123456", new DateTime(2020, 1, 1), new DateTime(2030, 1, 1));
            //documentService.CreateID(user, "123456", new DateTime(1990, 2, 3), new DateTime(2040, 4, 5));


            //foreach (Document doc in user.Documents)
            //{
            //    Console.WriteLine(doc.GetDetails());
            //}


            //UserAccountService us = new UserAccountService();
            //DocumentService dc = new DocumentService();


            //us.CreateUser("hola", "kala2");

            //User u1 = new(1, "sapun","middlename", "surname", Gender.Male, DateTime.Now,
            //    "London", "asd", "123", "fadf@email.com", Nationality.Chinese, UserType.Admin, false);

            //User u2 = new("uName", "pass", "email@email.com");

            //User u3 = new User(5, "uname", "p", "sname", Gender.NONE, DateTime.Now, "Manchester",
            //    "ppp", "ppp", null, Nationality.American, UserType.Admin, false);

            ////db.InsertUser(u3);


            //foreach (User user in us.GetAllUsers())
            //{
            //    if (user.Documents != null)
            //    {
            //        Console.WriteLine($"User with name: {user.Name}");
            //        foreach (Document doc in user.Documents)
            //        {
            //            Console.WriteLine(doc.GetDetails());
            //        }
            //    }
            //}


            //Console.WriteLine(us.GetUserByID(1).GetInfo());
            //Console.WriteLine(us.GetUserByID(5).GetInfo());
            //Console.WriteLine($"Document for UserID: {us.GetUserByID(5).Documents[0].GetDetails()}");

            //us.GetAllUsers();


            //foreach (string str in dc.CreatePassport(u2, "ZrSjf5SREC", DateTime.Now, DateTime.Now.AddDays(100)))
            //{
            //    Console.WriteLine($"{str}");
            //}

            //foreach (string str in dc.CreateID(u3, "77777771", DateTime.Now, DateTime.Now.AddDays(100)))
            //{
            //    Console.WriteLine($"{str}");
            //}


            //foreach (User user in us.GetAllUsers())
            //{
            //    Console.WriteLine(user.GetInfo());
            //}





            // Create a mock implementation of IPlaneDAL
            IPlaneDAL mockPlaneDAL = new PlaneDAL();
            ITicketsDAL ticketsDAL = new TicketsDAL();
            IUserAccountDAL userAccountDAL = new UserAccountDAL();
            IFlightDAL flightDAL = new FlightDAL();
            IAirportDAL airportDAL = new AirportDAL();

            FlightDAL flightDAL1 = new FlightDAL();


            ISeatAssignmentStrategy seatAssignmentStrategy;
            ISeatGroupCreator seatGroupCreator;


            //IPlaneSeatService planeSeatService = new PlaneSeatsService(seatGroupCreator, seatAssignmentStrategy);


            ////// the plane service must be 1 !
            ////IPlaneService _planeService = new PlaneService(mockPlaneDAL, ticketsDAL, planeSeatService);

            //IUserAccountService _userAccountService = new UserAccountService(userAccountDAL);
            //IFlightService _flightService = new FlightService(_planeService, ticketsDAL, flightDAL);
            //IAirportService _airportService = new AirportService(airportDAL);


            //Console.WriteLine(plane.ID);

            //foreach (Seat seat in _planeService.GetAvailableSeatsByFlightID(2))
            //{
            //    Console.WriteLine($"ROW: {seat.Row}, COLUMN: {seat.Column}");
            //}

            //Console.WriteLine(plane.CurrentLocation.TimeZone);

   //         void A320SeatAssign_Testing()
   //         {
   //             Plane plane = _planeService.GetPlaneByFlightID(2);
   //             ListSeats2(plane);

   //             Ticket ticket = ticketsDAL.GetTicketByID(8);

   //             if (_planeService.AutoAssignTicketToEconomySeat(2, ticket))
   //             {
   //                 Console.WriteLine($"ID:{ticket.ID} ticket assigned to {ticket.SeatRow}, {ticket.SeatColumn}");
   //             }
   //             else
   //             {
   //                 Console.WriteLine("[!] failed to assign ticket to next available seat");
   //             }


   //             Console.WriteLine("////////////////////////////////////////////");


   //             Plane planeUpdatedAfterSeat_DemoPurpose = _planeService.GetPlaneByFlightID(2);

   //             ListSeats2(planeUpdatedAfterSeat_DemoPurpose);
   //         }
   //         //A320SeatAssign_Testing();


   //         void A380SeatAssign_Testing()
   //         {
   //             Plane plane5 = _planeService.GetPlaneByFlightID(5);
   //             ListSeats2(plane5);

   //             Ticket ticket = ticketsDAL.GetTicketByID(5);

   //             if (_planeService.AutoAssignTicketToEconomySeat(5, ticket))
   //             {
   //                 Console.WriteLine($"ID:{ticket.ID} ticket assigned to {ticket.SeatRow}, {ticket.SeatColumn}");
   //             }
   //             else
   //             {
   //                 Console.WriteLine("[!] failed to assign ticket to next available seat");
   //             }


   //             Console.WriteLine("////////////////////////////////////////////");


   //             Plane planeUpdatedAfterSeat_DemoPurpose = _planeService.GetPlaneByFlightID(5);

   //             ListSeats2(planeUpdatedAfterSeat_DemoPurpose);
   //         }
   //         //A380SeatAssign_Testing();



   //         void GetFilterFlight()
   //         {
   //             DateTime Departure = new DateTime(2024, 04, 29);
   //             DateTime Return = new DateTime(2024, 04, 30);
   //             List<Flight> Flights = flightDAL1.GetAllFlightsByLocationTimeDate("Tokyo", "Dubai", Departure, Return);
   //             foreach (Flight flight in Flights)
   //             {
   //                 Console.WriteLine("Flight id: " + flight.FlightID);
   //             }
   //         }
   //         //GetFilterFlight();

			//Plane plane = _planeService.GetPlaneByFlightID(5);
   //         ListSeats2(plane);

            //foreach(Seat seat in _planeService.GetAvailableSeatsByFlightID(5))
            //{
            //    Console.WriteLine($"{seat.Id}: {seat.Taken}, {seat.SeatModel}");
            //}

			//User user = _userAccountService.GetUserByID(4);
			//ticketsDAL.CreateTicket(1, user.ID);

			//double price =_flightService.GetFlightPrice(2, SeatModel.Economy);
			//double price2 =_flightService.GetFlightPrice(2, SeatModel.Buisness);
			//double price3 =_flightService.GetFlightPrice(2, SeatModel.First);
			//Console.WriteLine(price.ToString());
			//Console.WriteLine(price2.ToString());
			//Console.WriteLine(price3.ToString());



			//Airport aiport = new("VFMK", "TEST", "TEST", "TEST", "TEST");
			//Console.WriteLine(_airportService.CreateAirport(aiport));

			//User user = new(15, "aaaaa", "aaaaa", "aaaaa", Enums.Gender.Male,
			//    DateOnly.FromDateTime(DateTime.Today), "aaaaa", "3", "3", "aaaaa@a.com",
			//    Enums.Nationality.American, Enums.UserType.Normal);

			//if (_userAccountService.UpdateUser(user) == true)
			//{
			//    Console.WriteLine("user updated");
			//}



			static void ListSeats2(Plane plane)
            {
                int maxRows = 0;
                Console.WriteLine("PLANE MODEL: " + plane.GetType());

                // Find the maximum number of rows among all groups
                foreach (SeatGroup group in plane.SeatGroups)
                {
                    maxRows = Math.Max(maxRows, group.Seats.GetLength(0));
                }

                for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
                {
                    List<(string seatString, ConsoleColor color)> formattedSeats = new List<(string seatString, ConsoleColor color)>();

                    foreach (SeatGroup group in plane.SeatGroups)
                    {
                        if (rowIndex < group.Seats.GetLength(0))
                        {

                            for (int j = 0; j < group.Seats.GetLength(1); j++)
                            {
                                Seat seat = group.Seats[rowIndex, j];
                                if (seat != null)
                                {
                                    ConsoleColor color;
                                    if (seat.Taken != false)
                                    {
                                        color = ConsoleColor.Red;
                                    }
                                    else
                                    {
                                        switch (seat.Type)
                                        {
                                            case SeatType.Window:
                                                color = ConsoleColor.Blue;
                                                break;
                                            case SeatType.Middle:
                                                color = ConsoleColor.Yellow;
                                                break;
                                            case SeatType.Path:
                                                color = ConsoleColor.Green;
                                                break;
                                            default:
                                                color = ConsoleColor.White;
                                                break;
                                        }
                                    }
                                    //formattedSeats.Add(($"[{seat.Row}{seat.Column}{seat.Id}{seat.Type.ToString()[0]}] ", color));
                                    formattedSeats.Add(($"[{seat.Row}{seat.Column}_{seat.Id}{seat.SeatModel}] ", color));
                                }
                                else
                                {
                                    // For null seats, mark as X and color red for consistency
                                    formattedSeats.Add(("[X] ", ConsoleColor.Red));
                                }
                            }
                        }
                    }

                    foreach (var (seatString, color) in formattedSeats)
                    {
                        Console.ForegroundColor = color;
                        Console.Write(seatString);
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();
            }

        }
    }
}
