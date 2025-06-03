using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Planes
{
    public class A320SeatAssignmentStrategy : ISeatAssignmentStrategy
    {
        public A320SeatAssignmentStrategy()
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
            int totalColumns = 0;
            foreach (var seatGroup in plane.SeatGroups)
            {
                for (int columnIndex = 0; columnIndex < seatGroup.Seats.GetLength(1); columnIndex++)
                {
                    char columnLetter = (char)('A' + totalColumns + (totalColumns >= 8 ? 1 : 0));
                    for (int rowIndex = 0; rowIndex < seatGroup.Seats.GetLength(0); rowIndex++)
                    {
                        var seat = seatGroup.Seats[rowIndex, columnIndex];
                        if (seat != null)
                        {
                            seat.SetSeatCOLUMN(columnLetter);
                        }
                    }
                    totalColumns++;
                }
            }
        }

        private void AssignSeatRows(Plane plane)
        {
            int maxRows = plane.SeatGroups.Max(g => g.Seats.GetLength(0));
            for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                foreach (var seatGroup in plane.SeatGroups)
                {
                    if (rowIndex < seatGroup.Seats.GetLength(0))
                    {
                        int rowNumber = rowIndex + 1;
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
