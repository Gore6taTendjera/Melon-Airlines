using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public static class PlaneHelper
    {
        public static Plane CreatePlane(PlaneModel model, int id, string registrationNumber, int capacity, Airport currentLocation, PlaneStatus status)
        {
            switch (model)
            {
                case PlaneModel.A320:
                    return new A320(id, registrationNumber, model, capacity, currentLocation, status);
                case PlaneModel.A380:
                    return new A380(id, registrationNumber, model, capacity, currentLocation, status);
                default:
                    throw new ArgumentException("Unsupported plane model.");
            }
        }
    }
}
