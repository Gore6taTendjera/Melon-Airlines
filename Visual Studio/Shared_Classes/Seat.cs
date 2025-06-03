using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Shared_Classes
{
    public class Seat
    {
        public int Id { get; private set; }
        public SeatType Type { get; private set; }
        public SeatModel SeatModel { get; private set; }
        public int Row { get; private set; }
        public char Column { get; private set; }
        public bool Taken { get; private set; }

        public Seat(SeatType seatType, SeatModel seatModel)
        {
            if (!Enum.IsDefined(typeof(SeatType), seatType))
                throw new ArgumentException("Invalid seat type.", nameof(seatType));

            if (!Enum.IsDefined(typeof(SeatModel), seatModel))
                throw new ArgumentException("Invalid seat model.", nameof(seatModel));

            this.Type = seatType;
            this.SeatModel = seatModel;
        }


        public Seat(int row, char column)
        {
            if (row <= 0)
                throw new ArgumentException("Row number must be a positive integer.", nameof(row));

            if (!char.IsLetter(column) || column < 'A' || column > 'Z')
                throw new ArgumentException("Column must be a letter between 'A' and 'Z'.", nameof(column));

            this.Row = row;
            this.Column = column;
        }


        public Seat(SeatType seatType, SeatModel seatModel, bool taken)
        {
            if (!Enum.IsDefined(typeof(SeatType), seatType))
                throw new ArgumentException("Invalid seat type.", nameof(seatType));

            if (!Enum.IsDefined(typeof(SeatModel), seatModel))
                throw new ArgumentException("Invalid seat model.", nameof(seatModel));

            this.Type = seatType;
            this.SeatModel = seatModel;
            this.Taken = taken;
        }


        public void SetSeatID(int id)
        {
            this.Id = id;
        }

        public void SetSeatROW(int row)
        {
            this.Row = row;
        }

        public void SetSeatCOLUMN(char column)
        {
            this.Column = column;
        }

        public void SetTaken(bool taken)
        {
            this.Taken = taken;
        }
    }
}
