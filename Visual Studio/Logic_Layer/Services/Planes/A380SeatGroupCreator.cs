using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Classes;
using Enums;

namespace Logic_Layer.Services.Planes
{
    public class A380SeatGroupCreator : ISeatGroupCreator
    {
        public A380SeatGroupCreator()
        {

        }

        public void CreateSeatGroups(Plane plane)
        {
            plane.SeatGroups.Add(new SeatGroup(3, 2, SeatGroupType.First, SeatModel.First));
            plane.SeatGroups.Add(new SeatGroup(3, 2, SeatGroupType.Last, SeatModel.First));

            plane.SeatGroups.Add(new SeatGroup(16, 2, SeatGroupType.First, SeatModel.Business));
            plane.SeatGroups.Add(new SeatGroup(16, 2, SeatGroupType.Middle, SeatModel.Business));
            plane.SeatGroups.Add(new SeatGroup(16, 2, SeatGroupType.First, SeatModel.Business));

            plane.SeatGroups.Add(new SeatGroup(31, 3, SeatGroupType.First, SeatModel.Economy));
            plane.SeatGroups.Add(new SeatGroup(31, 4, SeatGroupType.Middle, SeatModel.Economy));
            plane.SeatGroups.Add(new SeatGroup(31, 3, SeatGroupType.Last, SeatModel.Economy));
        }
    }
}
