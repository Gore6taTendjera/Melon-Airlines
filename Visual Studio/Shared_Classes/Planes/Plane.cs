using Shared_Classes;

public abstract class Plane
{
    public List<SeatGroup> SeatGroups { get; private set; }
    public int ID { get; private set; }
    public PlaneModel PlaneModel { get; private set; }
    public string RegistrationNumber { get; private set; }
    public int Capacity { get; private set; }
    public Airport CurrentLocation { get; private set; }
    public PlaneStatus CurrentStatus { get; private set; }

    protected Plane(PlaneModel planeModel, string registrationNumber, int capacity, Airport currentLocation, PlaneStatus currentStatus)
    {
        PlaneModel = planeModel;
        RegistrationNumber = registrationNumber;
        Capacity = capacity;
        CurrentLocation = currentLocation;
        CurrentStatus = currentStatus;
        SeatGroups = new List<SeatGroup>();
    }

    protected Plane(int id, PlaneModel planeModel, string registrationNumber, int capacity, Airport currentLocation, PlaneStatus currentStatus)
    : this(planeModel, registrationNumber, capacity, currentLocation, currentStatus)
    {
        ID = id;
        SeatGroups = new List<SeatGroup>();
    }

}