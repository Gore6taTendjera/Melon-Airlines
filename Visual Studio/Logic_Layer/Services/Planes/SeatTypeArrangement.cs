using Shared_Classes;
using Enums;

namespace Logic_Layer.Services.Planes
{
    public class SeatTypeArrangement : ISeatTypeArrangement
    {
        public SeatTypeArrangement()
        {
            
        }

        public void ArrangeAll(SeatGroup seatGroup, SeatModel seatModel) 
        {
            AssignPathSeats(seatGroup, seatModel);
            AssignMiddleSeats(seatGroup, seatModel);
            AssignWindowSeats(seatGroup, seatModel);
        }

        private void AssignWindowSeats(SeatGroup seatGroup, SeatModel seatModel)
        {
            int numRows = seatGroup.Seats.GetLength(0);
            int numColumns = seatGroup.Seats.GetLength(1);

            if (seatGroup.GroupType == SeatGroupType.First)
            {
                // Assign window seats to the first column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, 0] = new Seat(SeatType.Window, seatModel);
                }

                // Assign path seats to the last column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, numColumns - 1] = new Seat(SeatType.Path, seatModel);
                }

            }
            else if (seatGroup.GroupType == SeatGroupType.Last)
            {
                // Assign window seats to the last column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, numColumns - 1] = new Seat(SeatType.Window, seatModel);
                }

                // Assign path seats to the first column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, 0] = new Seat(SeatType.Path, seatModel);
                }

            }
        }

        private void AssignMiddleSeats(SeatGroup seatGroup, SeatModel seatModel)
        {
            int numRows = seatGroup.Seats.GetLength(0);
            int numColumns = seatGroup.Seats.GetLength(1);


            if (seatGroup.GroupType == SeatGroupType.First)
            {
                // Assign middle seats for each row
                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 1; j < numColumns - 1; j++) // Start from the second column and end before the last column
                    {
                        seatGroup.Seats[i, j] = new Seat(SeatType.Middle, seatModel);
                    }
                }
            }

            else if (seatGroup.GroupType == SeatGroupType.Last)
            {
                // Assign middle seats for each row
                for (int i = 0; i < numRows; i++)
                {
                    for (int j = numColumns - 2; j > 0; j--) // Start from the second-to-last column and end before the first column
                    {
                        seatGroup.Seats[i, j] = new Seat(SeatType.Middle, seatModel);
                    }
                }
            }

            else if (seatGroup.GroupType == SeatGroupType.Middle)
            {
                // Assign middle seats for each row
                for (int i = 0; i < numRows; i++)
                {
                    for (int j = numColumns - 2; j > 0; j--) // Start from the second-to-last column and end before the first column
                    {
                        seatGroup.Seats[i, j] = new Seat(SeatType.Middle, seatModel);
                    }
                }
            }
        }

        private void AssignPathSeats(SeatGroup seatGroup, SeatModel seatModel)
        {
            int numRows = seatGroup.Seats.GetLength(0);
            int numColumns = seatGroup.Seats.GetLength(1);

            if (seatGroup.GroupType == SeatGroupType.First)
            {
                // Assign path seats to the last column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, numColumns - 1] = new Seat(SeatType.Path, seatModel);
                }
            }

            else if (seatGroup.GroupType == SeatGroupType.Middle)
            {
                // Assign path seats to the first column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, 0] = new Seat(SeatType.Path, seatModel);
                }

                // Assign path seats to the last column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, numColumns - 1] = new Seat(SeatType.Path, seatModel);
                }
            }

            else if (seatGroup.GroupType == SeatGroupType.Last)
            {
                // Assign path seats to the first column of every row
                for (int i = 0; i < numRows; i++)
                {
                    seatGroup.Seats[i, 0] = new Seat(SeatType.Path, seatModel);
                }
            }

        }

    }
}