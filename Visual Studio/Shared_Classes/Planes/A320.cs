using Shared_Classes;

public class A320 : Plane
{

    public A320(int id, string registrationNumber, PlaneModel planeModel, int capacity, Airport currentLocation, PlaneStatus currentStatus)
        : base(id, planeModel, registrationNumber, capacity, currentLocation, currentStatus)
    {

    }

}