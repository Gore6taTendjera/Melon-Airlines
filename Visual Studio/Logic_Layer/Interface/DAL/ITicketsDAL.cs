using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Logic_Layer.Interface.DAL
{
    public interface ITicketsDAL
    {

        bool CreateTicket(int flightID, int userID);
        bool CreateTicket(int flightID, int userID, SeatModel seatModel, int seatRow, char seatColumn);


        Ticket GetTicketByID(int ticketId);
        List<Ticket> GetAllTicketsByPassengerID(int passengerId);

        List<Ticket> GetAllTickets();
        List<Ticket> GetAllTicketsByFlightID(int flightId);

        bool UpdateTicket(Ticket ticket);
        bool DeleteTicketByID(int ticketId);


    }
}
