using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Planes
{
    public interface ISeatGroupCreator
    {
        void CreateSeatGroups(Plane plane);
    }
}
