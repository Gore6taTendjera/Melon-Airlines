using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Planes
{
    public interface IPlaneSeatService
    {
        void ArrangeSeats(Plane plane); // arrange seat configuration for other plane.
    }
}
