using DTOs;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Shared_Classes;
using System.Collections.Generic;
using Enums;

namespace Logic_Layer
{
    public class TicketService : ITicketService
    {
        private readonly ITicketsDAL _ticketsDAL;

        public TicketService(ITicketsDAL ticketsDAL)
        {
            _ticketsDAL = ticketsDAL;
        }


        // reserve flight without seat
        public bool CreateTicket(int flightID, int userID)
        {
            return _ticketsDAL.CreateTicket(flightID, userID);
        }  
        
        public bool CreateTicket(int flightID, int userID, SeatModel seatModel, int seatRow, char seatColumn)
        {
            return _ticketsDAL.CreateTicket(flightID, userID, seatModel, seatRow, seatColumn);
        }



        public List<Ticket> GetAllTickets()
        {
            return _ticketsDAL.GetAllTickets();
        }

        public Ticket GetTicketById(int ticketId)
        {
            return _ticketsDAL.GetTicketByID(ticketId);
        }

        public List<Ticket> GetAllTicketsByPassengerID(int passengerId)
        {
            return _ticketsDAL.GetAllTicketsByPassengerID(passengerId);
        }

        public List<TicketDTO> GetAllTicketsByPassengerIdDTO(int passengerId)
        {
            List<Ticket> tickets = _ticketsDAL.GetAllTicketsByPassengerID(passengerId);
            List<TicketDTO> ticketDTOs = new List<TicketDTO>();

            foreach (Ticket ticket in tickets)
            {
                TicketDTO ticketDTO = new TicketDTO
                {
                    FlightID = ticket.FlightID,

                    SeatRow = ticket.SeatRow,
                    SeatColumn = ticket.SeatColumn,
                    SeatModel = ticket.SeatModel,
                };
                ticketDTOs.Add(ticketDTO);
            }
            return ticketDTOs;
        }

        public Ticket GetTicketByID(int ticketId)
        {
            return _ticketsDAL.GetTicketByID(ticketId);
        }

        public List<Ticket> GetAllTicketsByFlightID(int flightId)
        {
            return _ticketsDAL.GetAllTicketsByFlightID(flightId);
        }



        public bool UpdateTicket(Ticket ticket)
        {
            return _ticketsDAL.UpdateTicket(ticket);
        }


        public bool DeleteTicketByID(int ticketId)
        {
            return _ticketsDAL.DeleteTicketByID(ticketId);
        }
   

   
    }
}
