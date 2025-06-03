using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services.Planes;
using Shared_Classes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Enums;
using DTOs;

namespace Logic_Layer
{
    public class PlaneService : IPlaneService
    {
        private readonly IPlaneDAL _planeDAL;
        private readonly ITicketService _ticketsService;
        private readonly IPlaneSeatsServiceFactory _planeSeatsServiceFactory;

        public PlaneService(IPlaneDAL planeDAL, ITicketService ticketService, IPlaneSeatsServiceFactory planeSeatService)
        {
            _planeDAL = planeDAL;
			_ticketsService = ticketService;
            _planeSeatsServiceFactory = planeSeatService;
        }


        public bool CreateNewPlane(Plane plane)
        {
            return _planeDAL.CreateNewPlane(plane);
        }


        public List<Plane> GetAllPlanes() 
        {
            return _planeDAL.GetAllPlanes();
        }

        public Plane GetPlaneByFlightID(int flightID)
        {
            // db to get tickets and
            // if ticket matches the flight id and the plane id
            // reserve the plane seats.
            Plane plane = _planeDAL.GetPlaneByFlightID(flightID);
            _planeSeatsServiceFactory.CreateService(plane.PlaneModel).ArrangeSeats(plane);
            List<Ticket> tickets = _ticketsService.GetAllTicketsByFlightID(flightID);
            foreach (Ticket ticket in tickets)
            {
                AssignTicketToSeat(plane, ticket);
            }

            return plane;
        }

        private void AssignTicketToSeat(Plane plane, Ticket ticket)
        {
            foreach (SeatGroup group in plane.SeatGroups)
            {
                for (int i = 0; i < group.Seats.GetLength(0); i++)
                {
                    for (int j = 0; j < group.Seats.GetLength(1); j++)
                    {
                        Seat seat = group.Seats[i, j];
                        if (seat != null && seat.Taken == false && seat.Row == ticket.SeatRow && seat.Column == ticket.SeatColumn)
                        {
                            seat.SetTaken(true);
                        }
                    }
                }
            }
        }


        public PlaneDTO GetPlaneByFlightIDDTO(int flightID)
		{
			Plane plane = _planeDAL.GetPlaneByFlightID(flightID);

            _planeSeatsServiceFactory.CreateService(plane.PlaneModel).ArrangeSeats(plane);

            List<Ticket> tickets = _ticketsService.GetAllTicketsByFlightID(flightID);

			foreach (Ticket ticket in tickets)
			{
				AssignTicketToSeat(plane, ticket);
			}

			PlaneDTO dto = new PlaneDTO
			{
				ID = plane.ID,
				SeatGroups = plane.SeatGroups,
				PlaneModel = plane.PlaneModel
			};

			return dto;
		}


		public Plane GetPlaneByID(int id)
        {
            Plane plane = _planeDAL.GetPlaneByID(id);
			return plane;
        }



        public bool DeletePlane(int planeID)
        {
            return _planeDAL.DeletePlaneByID(planeID);
        }

    }
}
