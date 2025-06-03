using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Classes;
using Enums;

namespace Logic_Layer.Services.Planes
{
    public class A320SeatGroupCreator : ISeatGroupCreator
    {
        public A320SeatGroupCreator()
        {
            
        }
        public void CreateSeatGroups(Plane plane)
        {
            plane.SeatGroups.Add(new SeatGroup(30, 3, SeatGroupType.First, SeatModel.Economy));
            plane.SeatGroups.Add(new SeatGroup(30, 3, SeatGroupType.Last, SeatModel.Economy));
        }
    }
}
