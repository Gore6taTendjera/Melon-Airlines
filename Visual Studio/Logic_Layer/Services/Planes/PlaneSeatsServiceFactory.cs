using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Planes
{
    public class PlaneSeatsServiceFactory : IPlaneSeatsServiceFactory
    {
        public PlaneSeatsService CreateService(PlaneModel planeModel)
		{
			switch (planeModel)
			{
				case PlaneModel.A320:

					SeatTypeArrangement seatTypeArrangementA320 = new();
					return new PlaneSeatsService(new A320SeatGroupCreator(), new A320SeatAssignmentStrategy(), seatTypeArrangementA320);

				case PlaneModel.A380:

					SeatTypeArrangement seatTypeArrangementA380 = new();
					return new PlaneSeatsService(new A380SeatGroupCreator(), new A380SeatAssignmentStrategy(), seatTypeArrangementA380);

				default:
					throw new ArgumentException("Unsupported plane type");
			}
		}
	}
}
