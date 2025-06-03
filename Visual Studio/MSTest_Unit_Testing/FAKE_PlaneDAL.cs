using Logic_Layer.Interface.DAL;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uMSTest_Unit_Testing
{
    public class FAKE_PlaneDAL : IPlaneDAL
    {
        private Dictionary<int, Plane> _planes = new Dictionary<int, Plane>
        {
            {1, new A320(1, "VFNKL", PlaneModel.A320, 200, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London"), PlaneStatus.InService)},
            {2, new A380(2, "BRTUR", PlaneModel.A380, 400, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London"), PlaneStatus.Maintenance)},
            {3, new A320(1, "VEOI3", PlaneModel.A320, 300, new Airport(1, "KCD", "Heatrow", "London", "UK", "UK/London"), PlaneStatus.Maintenance)}
        };

        public bool CreateNewPlane(Plane plane)
        {
            if (plane != null)
            {
                _planes.Add(plane.ID, plane);
                return true;
            }
            return false;
        }

        public bool DeletePlaneByID(int planeId)
        {
            if (_planes.ContainsKey(planeId))
            {
                _planes.Remove(planeId);
                return true;
            }
            return false;
        }

        public List<Plane> GetAllPlanes()
        {
            List<Plane> planeList = new List<Plane>();
            foreach (var plane in _planes.Values)
            {
                planeList.Add(plane);
            }
            return planeList;
        }


        public Plane GetPlaneByFlightID(int flightID)
        {
            foreach (var plane in _planes.Values)
            {
                if (plane.ID == flightID)
                {
                    return plane;
                }
            }
            return null;
        }

        public Plane GetPlaneByID(int id)
        {
            if (_planes.ContainsKey(id))
            {
                return _planes[id];
            }
            return null;
        }

        public bool UpdatePlane(Plane plane)
        {
            if (_planes.ContainsKey(plane.ID))
            {
                _planes[plane.ID] = plane;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
