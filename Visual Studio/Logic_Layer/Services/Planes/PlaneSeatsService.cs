using Shared_Classes;

namespace Logic_Layer.Services.Planes
{
    public class PlaneSeatsService : IPlaneSeatService
    {
        private readonly ISeatAssignmentStrategy _seatAssignmentStrategy;
        private readonly ISeatGroupCreator _seatGroupCreator;
        private readonly ISeatTypeArrangement _seatTypeArrangement;


        // Created by factory PlaneSeatsServiceFactory.cs
        public PlaneSeatsService(ISeatGroupCreator seatGroupCreator, ISeatAssignmentStrategy seatAssignmentStrategy, ISeatTypeArrangement seatTypeArrangement)
        {
            _seatAssignmentStrategy = seatAssignmentStrategy;
            _seatGroupCreator = seatGroupCreator;
            _seatTypeArrangement = seatTypeArrangement;
        }

		public void ArrangeSeats(Plane plane)
		{
			// seat groups are always created before assignSeats
			// seat types (Window, Middle, Path) are arranged.
			// because SeatAssign is using the groups (seat configurations)

			_seatGroupCreator.CreateSeatGroups(plane);
			foreach (SeatGroup seatGroup in plane.SeatGroups)
			{
				_seatTypeArrangement.ArrangeAll(seatGroup, seatGroup.SeatModel);
			}
			_seatAssignmentStrategy.AssignSeats(plane);

		}
	}
}
