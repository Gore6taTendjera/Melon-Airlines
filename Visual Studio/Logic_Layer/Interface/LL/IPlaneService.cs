using DTOs;
using Shared_Classes;

namespace Logic_Layer.Interface.LL
{
    public interface IPlaneService
    {
        bool CreateNewPlane(Plane plane);


        List<Plane> GetAllPlanes();
        Plane GetPlaneByID(int id);
        Plane GetPlaneByFlightID(int flightID);
        PlaneDTO GetPlaneByFlightIDDTO(int flightID);
        

        bool DeletePlane(int planeID);
    }
}