using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.DAL
{
    public interface IUserAccountDAL
    {
        bool InsertUser(User user);

        bool UsernameExists(string username);
        List<User> GetAllUsers();
        User GetUserById(int id);
        User Authenticate(string username);

        bool UpdateUser(User user);

        bool DeleteUser(int id);

    }
}
