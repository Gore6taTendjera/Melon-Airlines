using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.DAL
{
    public interface IAirportDAL
    {
        bool CreateAirport(Airport airport);

        List<Airport> GetAllAirports();
        Airport GetAirportByID(int ID);

        bool DeleteAirportByID(int ID);

    }
}
