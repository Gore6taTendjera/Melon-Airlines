using DTOs;
using Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interface.LL
{
    public interface IUserAccountService
    {
        bool Register(string username, string pass, string email);
        bool CreateUser(User user);

        List<User> GetAllUsers();
        User GetUserByID(int id);
        UserLoginDTO Authenticate(string username, string password);
        UserProfileDetailsDTO GetUserProfileDetails(int id);
        User GetUserByUsername(string username);

        bool UpdateUser(User user);
        bool UpdateUserDetails(UserProfileDetailsDTO userDetails);

        bool DeleteUser(int id);


    }
}
