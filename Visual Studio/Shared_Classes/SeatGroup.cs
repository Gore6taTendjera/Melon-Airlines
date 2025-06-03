using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Shared_Classes
{
    public class SeatGroup
    {
        public Seat[,] Seats { get; private set; }
        public SeatGroupType GroupType { get; private set; }
        public SeatModel SeatModel { get; private set; }


        public SeatGroup(int rows, int columns, SeatGroupType groupType, SeatModel seatModel)
        {
            if (rows <= 0)
                throw new ArgumentException("Number of rows must be greater than zero.", nameof(rows));

            if (columns <= 0)
                throw new ArgumentException("Number of columns must be greater than zero.", nameof(columns));

            if (!Enum.IsDefined(typeof(SeatGroupType), groupType))
                throw new ArgumentException("Invalid seat group type.", nameof(groupType));

            if (!Enum.IsDefined(typeof(SeatModel), seatModel))
                throw new ArgumentException("Invalid seat model.", nameof(seatModel));

            this.GroupType = groupType;
            this.Seats = new Seat[rows, columns];
            this.SeatModel = seatModel;
        }

    }
}
