using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Shared_Classes
{
    public class Ticket
    {
        public int ID { get; private set; }
        public int UserID { get; private set; }
        public int FlightID { get; private set; }
        public SeatModel? SeatModel { get; private set; }
        public int? SeatRow { get; private set; }
        public char? SeatColumn { get; private set; }



        // on User booking:
        // create ticket without seat.
        public Ticket(int userID, int flightID)
        {
            if (userID <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userID));

            if (flightID <= 0)
                throw new ArgumentException("Flight ID must be a positive integer.", nameof(flightID));

            this.UserID = userID;
            this.FlightID = flightID;
        }


        // create ticket with seat.
        // When user selects the First/Business/Economy seat
        // SeatModel is automatically picked by the ALG that returns the seats
        public Ticket(int userID, int flightID, SeatModel seatModel, int row, char column)
        {
            if (userID <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userID));

            if (flightID <= 0)
                throw new ArgumentException("Flight ID must be a positive integer.", nameof(flightID));

            if (!Enum.IsDefined(typeof(SeatModel), seatModel))
                throw new ArgumentException("Invalid seat model.", nameof(seatModel));

            if (row <= 0)
                throw new ArgumentException("Seat row must be a positive integer.", nameof(row));

            if (!char.IsLetter(column) || column < 'A' || column > 'Z')
                throw new ArgumentException("Seat column must be a letter between 'A' and 'Z'.", nameof(column));

            this.UserID = userID;
            this.FlightID = flightID;
            this.SeatModel = seatModel;
            this.SeatRow = row;
            this.SeatColumn = column;
        }



        // db get
        // 1 ctor instead of 2 separate for both Non-Seat and Seat allocated
        public Ticket(int ticketID, int userID, int flightID, SeatModel? seatModel, int? seatRow, char? seatColumn)
        {
            if (ticketID < 0)
                throw new ArgumentException("Ticket ID must be a non-negative integer.", nameof(ticketID));

            if (userID <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userID));

            if (flightID <= 0)
                throw new ArgumentException("Flight ID must be a positive integer.", nameof(flightID));

            this.ID = ticketID;
            this.UserID = userID;
            this.FlightID = flightID;
            this.SeatModel = seatModel;
            this.SeatRow = seatRow;
            this.SeatColumn = seatColumn;
        }


        public void SetSeatRow(int seatRow)
        {
            this.SeatRow = seatRow;
        }  

        public void SetSeatColumn(char seatColumn)
        {
            this.SeatColumn = seatColumn;
        }
    }
}
