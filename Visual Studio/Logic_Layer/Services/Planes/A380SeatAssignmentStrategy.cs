using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer;
using Shared_Classes;
using Enums;

namespace Logic_Layer.Services.Planes
{
    public class A380SeatAssignmentStrategy : ISeatAssignmentStrategy
    {
        public A380SeatAssignmentStrategy()
        {
            
        }

        public void AssignSeats(Plane plane)
        {
            AssignSeatIds(plane);
            AssignSeatColumns(plane);
            AssignSeatRows(plane);
        }

        private void AssignSeatIds(Plane plane)
        {
            int seatId = 1; // Initialize the seat ID for the entire plane
            int maxRows = 0;

            foreach (SeatGroup seatGroup in plane.SeatGroups)
            {
                maxRows = Math.Max(maxRows, seatGroup.Seats.GetLength(0));
            }

            // Iterate over each row of all groups
            for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                // Increment the seat IDs for the current row of all groups
                foreach (SeatGroup seatGroup in plane.SeatGroups)
                {
                    // Check if the current row index is within the bounds of the group's seat array
                    if (rowIndex < seatGroup.Seats.GetLength(0))
                    {
                        for (int j = 0; j < seatGroup.Seats.GetLength(1); j++)
                        {
                            Seat seat = seatGroup.Seats[rowIndex, j];
                            if (seat != null && seat.Type == SeatType.Path)
                            {
                                seat.SetSeatID(seatId);
                                seatId++;
                            }
                        }
                    }
                }
            }
            for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                // Increment the seat IDs for the current row of all groups
                foreach (SeatGroup seatGroup in plane.SeatGroups)
                {
                    // Check if the current row index is within the bounds of the group's seat array
                    if (rowIndex < seatGroup.Seats.GetLength(0))
                    {
                        for (int j = 0; j < seatGroup.Seats.GetLength(1); j++)
                        {
                            Seat seat = seatGroup.Seats[rowIndex, j];
                            if (seat != null && seat.Type == SeatType.Window)
                            {
                                seat.SetSeatID(seatId);
                                seatId++;
                            }
                        }
                    }
                }
            }
            for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                // Increment the seat IDs for the current row of all groups
                foreach (SeatGroup seatGroup in plane.SeatGroups)
                {
                    // Check if the current row index is within the bounds of the group's seat array
                    if (rowIndex < seatGroup.Seats.GetLength(0))
                    {
                        for (int j = 0; j < seatGroup.Seats.GetLength(1); j++)
                        {
                            Seat seat = seatGroup.Seats[rowIndex, j];
                            if (seat != null && seat.Type == SeatType.Middle)
                            {
                                seat.SetSeatID(seatId);
                                seatId++;
                            }
                        }
                    }
                }
            }
        }

        private void AssignSeatColumns(Plane plane)
        {
            int groupIndex = 0;
            int totalColumnsA380 = 0;
            foreach (var seatGroup in plane.SeatGroups)
            {
                int numRows = seatGroup.Seats.GetLength(0);
                int numColumns = seatGroup.Seats.GetLength(1);
                char[] columns;
                switch (seatGroup.SeatModel)
                {
                    // Based on the model First & Business class seats have the following seat column '.'
                    // https://www.koreanair.com/nl/en/in-flight/aircraft/a380/800-407/seat-map
                    case SeatModel.First:
                        columns = groupIndex == 0 ? new[] { 'A', 'D' } : new[] { 'E', 'J' };
                        break;
                    case SeatModel.Business:
                        columns = groupIndex == 2 ? new[] { 'A', 'B' } : groupIndex == 3 ? new[] { 'D', 'E' } : new[] { 'G', 'H' };
                        break;
                    default:
                        columns = null;
                        break;
                }

                if (columns != null)
                {
                    for (int columnIndex = 0; columnIndex < numColumns; columnIndex++)
                    {
                        for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
                        {
                            var seat = seatGroup.Seats[rowIndex, columnIndex];
                            if (seat != null)
                            {
                                seat.SetSeatCOLUMN(columns[columnIndex]);
                            }
                        }
                    }
                    groupIndex++;
                }
                else if (seatGroup.SeatModel == SeatModel.Economy)
                {
                    for (int columnIndex = 0; columnIndex < numColumns; columnIndex++)
                    {
                        char columnLetter = (char)('A' + totalColumnsA380 + (totalColumnsA380 >= 8 ? 1 : 0));
                        for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
                        {
                            var seat = seatGroup.Seats[rowIndex, columnIndex];
                            if (seat != null)
                            {
                                seat.SetSeatCOLUMN(columnLetter);
                            }
                        }
                        totalColumnsA380++;
                    }
                }
            }
        }

        private void AssignSeatRows(Plane plane)
        {
            int maxRows = plane.SeatGroups.Max(g => g.Seats.GetLength(0));
            int firstRowNumber = 1;
            int businessRowNumber = 4;
            int economyRowNumber = 20;

            for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                foreach (var seatGroup in plane.SeatGroups)
                {
                    int rowNumber = 0;
                    switch (seatGroup.SeatModel)
                    {
                        case SeatModel.First:
                            rowNumber = firstRowNumber + rowIndex;
                            break;
                        case SeatModel.Business:
                            rowNumber = businessRowNumber + rowIndex;
                            break;
                        case SeatModel.Economy:
                            rowNumber = economyRowNumber + rowIndex;
                            break;
                    }

                    if (rowNumber > 0 && rowIndex < seatGroup.Seats.GetLength(0))
                    {
                        for (int columnIndex = 0; columnIndex < seatGroup.Seats.GetLength(1); columnIndex++)
                        {
                            var seat = seatGroup.Seats[rowIndex, columnIndex];
                            if (seat != null)
                            {
                                seat.SetSeatROW(rowNumber);
                            }
                        }
                    }
                }
            }
        }
    }
}
