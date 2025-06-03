using DTOs;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Planes
{
    public interface ISeatService
    {
  
        List<Seat> GetAllSeatsByFlightID(int flightID);
        List<Seat> GetAvailableSeatsByFlightID(int flightID);
        List<Seat> GetAllSeatsByFlightPlaneDTO(PlaneDTO planeDTO);


        Seat GetAvailableFirstSeatByFlightID(int flightID);
        Seat GetAvailableBusinessSeatByFlightID(int flightID);
        Seat GetAvailableEconomySeatByFlightID(int flightID);

    }
}
