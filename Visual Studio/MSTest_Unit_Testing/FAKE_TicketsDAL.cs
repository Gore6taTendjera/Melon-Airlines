using Logic_Layer.Interface.DAL;
using Shared_Classes;
using System.Collections.Generic;
using Enums;

namespace uMSTest_Unit_Testing
{
    public class FAKE_TicketsDAL : ITicketsDAL
    {
        private Dictionary<int, Ticket> _tickets = new Dictionary<int, Ticket>
        {
            {1, new Ticket(1, 123, 101, SeatModel.Economy, 20, 'B')},
            {2, new Ticket(1, 123, 75, SeatModel.Economy, 12, 'D')},
            {3, new Ticket(2, 456, 102, SeatModel.Business, 10, 'A')}
        };

        public bool CreateTicket(int flightID, int userID)
        {
            _tickets.Add(4, new Ticket(flightID, userID, 0, SeatModel.Economy, 0, '0'));
            return true;
        }

        public bool CreateTicket(int flightID, int userID, SeatModel seatModel, int seatRow, char seatColumn)
        {
            _tickets.Add(4, new Ticket(flightID, userID, 0, seatModel, seatRow, seatColumn));
            return true;
        }
        public Ticket GetTicketByID(int ticketId)
        {
            if (_tickets.ContainsKey(ticketId))
            {
                return _tickets[ticketId];
            }
            return null;
        }

        public List<Ticket> GetAllTicketsByPassengerID(int passengerId)
        {
            List<Ticket> tickets = new List<Ticket>();
            foreach (Ticket ticket in _tickets.Values)
            {
                if (ticket.UserID == passengerId)
                {
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }

        public List<Ticket> GetAllTickets()
        {
            return new List<Ticket>(_tickets.Values);
        }

        public List<Ticket> GetAllTicketsByFlightID(int flightId)
        {
            List<Ticket> tickets = new List<Ticket>();
            foreach (Ticket ticket in _tickets.Values)
            {
                if (ticket.FlightID == flightId)
                {
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }

        public bool UpdateTicket(Ticket ticket)
        {
            if (_tickets.ContainsKey(ticket.ID))
            {
                _tickets[ticket.ID] = ticket;
                return true;
            }
            return false;
        }

        public bool DeleteTicketByID(int ticketId)
        {
            return _tickets.Remove(ticketId);
        }
    }
}
