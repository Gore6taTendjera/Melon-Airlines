using DTOs;
using Enums;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Shared_Classes;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic_Layer
{
    public class UserAccountService : IUserAccountService
    {
        private IUserAccountDAL _userAccountDAL;

        public UserAccountService(IUserAccountDAL userAccountDAL)
        {
            this._userAccountDAL = userAccountDAL;
        }

        public bool CreateUser(User user)
        {
            return _userAccountDAL.InsertUser(user);
        }




        public bool Register(string username, string password, string email)
        {
            if (!UsernameExists(username))
            {
                string hashedPassword = PasswordHelper.HashPassword(password);
                User user = new User(username, hashedPassword, email);
                return _userAccountDAL.InsertUser(user);
            }
            else
            {
                return false;
            }
        }

        private bool UsernameExists(string username)
        {
            return _userAccountDAL.UsernameExists(username);
        }




        public List<User> GetAllUsers()
        {
            return _userAccountDAL.GetAllUsers();
        }

        public User GetUserByID(int id)
        {
            return _userAccountDAL.GetUserById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _userAccountDAL.Authenticate(username);
        }

        public UserLoginDTO Authenticate(string username, string password)
        {
            User user = _userAccountDAL.Authenticate(username);

            if (user != null && PasswordHelper.VerifyPassword(password, user.Password))
            {
                return new UserLoginDTO
                {
                    ID = user.ID,
                    Username = user.Username,
                    Password = password,
                    UserType = user.UserType
                };
            }
            else
            {
                return null;
            }
        }

        public UserProfileDetailsDTO GetUserProfileDetails(int id)
        {
            User user = _userAccountDAL.GetUserById(id);
            if (user != null)
            {
                return new UserProfileDetailsDTO
                {
                    Name = user.Name,
                    MiddleName = user.MiddleName,
                    Surname = user.Surname,
                    Gender = user.Gender,
                    // Convert DateOnly? to DateTime?
                    BirthDate = user.BirthDate.HasValue ? new DateTime(user.BirthDate.Value.Year, user.BirthDate.Value.Month, user.BirthDate.Value.Day) : (DateTime?)null,
                    BirthPlace = user.BirthPlace,
                    Email = user.Email,
                    Nationality = user.Nationality
                };
            }
            else
            {
                return null;
            }
        }


        public bool UpdateUserDetails(UserProfileDetailsDTO userDetails)
        {
            // Convert DateTime? to DateOnly?
            DateOnly? birthDate = userDetails.BirthDate.HasValue ? new DateOnly(userDetails.BirthDate.Value.Year, userDetails.BirthDate.Value.Month, userDetails.BirthDate.Value.Day) : (DateOnly?)null;

            User user = new User(userDetails.ID, userDetails.Name, userDetails.MiddleName, userDetails.Surname, userDetails.Gender,
                birthDate, userDetails.BirthPlace, userDetails.Email, userDetails.Nationality);

            return _userAccountDAL.UpdateUser(user);
        }



        public bool UpdateUser(User user)
        {
            return _userAccountDAL.UpdateUser(user);
        }



        public bool DeleteUser(int id)
        {
            return _userAccountDAL.DeleteUser(id);
        }


    }
}
