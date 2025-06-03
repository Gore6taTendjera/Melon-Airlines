using Shared_Classes;

public class A380 : Plane
{

    public A380(int id, string registrationNumber, PlaneModel planeModel, int capacity, Airport currentLocation, PlaneStatus currentStatus)
        : base(id, planeModel, registrationNumber, capacity, currentLocation, currentStatus)
    {

    }

}