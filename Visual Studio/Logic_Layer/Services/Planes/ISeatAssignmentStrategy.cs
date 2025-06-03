using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Planes
{
    public interface ISeatAssignmentStrategy
    {
        void AssignSeats(Plane plane); // can be extendable because it's your choise how to assign the seats.
    }
}
