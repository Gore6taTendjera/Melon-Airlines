using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public abstract class Base
    {
        protected string connectionString = "Data Source = DESKTOP-L26R18B\\SQLEXPRESS; Initial Catalog=Individual; User Id=asd; Password=asd; TrustServerCertificate=True";
        //protected string connectionString = "Data Source = mssqlstud.fhict.local; Initial Catalog = dbi540905_melonlines; User Id = dbi540905_melonlines; Password=123; TrustServerCertificate=True";

    }
}
