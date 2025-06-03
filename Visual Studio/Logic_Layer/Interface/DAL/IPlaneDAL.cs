namespace Logic_Layer.Interface.DAL
{
    public interface IPlaneDAL
    {
        bool CreateNewPlane(Plane plane);

        List<Plane> GetAllPlanes();
        Plane GetPlaneByFlightID(int flightID);
        Plane GetPlaneByID(int id);

        bool UpdatePlane(Plane plane);

        bool DeletePlaneByID(int planeId);
    }
}
