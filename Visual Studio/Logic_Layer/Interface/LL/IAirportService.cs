using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.LL
{
    public interface IAirportService
    {
        bool CreateAirport(Airport airport);

        List<Airport> GetAllAirports();
        Airport GetAirportByID(int ID);

        bool DeleteAirportByID(int ID);

    }
}
