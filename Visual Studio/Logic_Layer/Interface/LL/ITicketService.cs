using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Classes;
using Enums;
using DTOs;

namespace Logic_Layer.Interface.LL
{
    public interface ITicketService
    {


        bool CreateTicket(int flightID, int userID);
        bool CreateTicket(int flightID, int userID, SeatModel seatModel, int seatRow, char seatColumn);



        Ticket GetTicketByID(int ticketId);
        List<Ticket> GetAllTicketsByPassengerID(int passengerId);
        List<TicketDTO> GetAllTicketsByPassengerIdDTO(int passengerId);

        List<Ticket> GetAllTickets();
        List<Ticket> GetAllTicketsByFlightID(int flightId);

        bool UpdateTicket(Ticket ticket);
        bool DeleteTicketByID(int ticketId);

    }
}
