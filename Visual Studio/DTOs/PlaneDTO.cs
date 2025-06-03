using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PlaneDTO
    {
        public int ID { get; set; }
        public List<SeatGroup> SeatGroups { get; set; }
        public PlaneModel PlaneModel { get; set; }

    }
}
