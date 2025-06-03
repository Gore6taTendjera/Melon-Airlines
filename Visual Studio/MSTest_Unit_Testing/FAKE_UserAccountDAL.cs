using Enums;
using System;
using System.Collections.Generic;
using Logic_Layer.Interface.DAL;
using Shared_Classes;

namespace Data_Access_Layer
{
    public class FAKE_UserAccountDAL : Base, IUserAccountDAL
    {
        private Dictionary<int, User> _users = new Dictionary<int, User>
        {
            { 1, new User(1, "John", "Doe", "Smith", Gender.Male, new DateOnly(1990, 1, 1), "New York", "v7", "$2a$11$cCmTL/cWCwV6gUrRd.M6.e2wcoM5XcgnIWPpCtU2EYHz4pz7jQlTG", "john@example.com", Nationality.American, UserType.Normal) },
            { 2, new User(2, "Jane", "Doe", "Smith", Gender.Female, new DateOnly(1995, 5, 10), "Los Angeles", "jane_doe", "pass123", "jane@example.com", Nationality.American, UserType.Normal) },
            { 3, new User(3, "Alice", "Wonder", "Liddell", Gender.Female, new DateOnly(1987, 3, 15), "London", "alice123", "abc123", "alice@example.com", Nationality.English, UserType.Admin) },
            { 4, new User(4, "Bob", "Builder", "Johnson", Gender.Male, new DateOnly(1980, 8, 20), "Manchester", "bob_build", "qwerty", "bob@example.com", Nationality.English, UserType.Normal) },
            { 5, new User(5, "Charlie", "Chaplin", "", Gender.Male, new DateOnly(1975, 12, 5), "London", "charliec", "charlie123", "charlie@example.com", Nationality.English, UserType.Normal) }
        };

        public User Authenticate(string username)
        {
            foreach (var user in _users.Values)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }

            // User not found
            return null;
        }


        public bool DeleteUser(int id)
        {
            return _users.Remove(id);
        }

        public List<User> GetAllUsers()
        {
            return new List<User>(_users.Values);
        }

        public User GetUserById(int id)
        {
            if (_users.TryGetValue(id, out User user))
            {
                return user;
            }
            return null;
        }

        public bool InsertUser(User user)
        {
            if (_users.ContainsKey(user.ID))
            {
                return false; // already exists
            }
            _users.Add(user.ID, user);
            return true;
        }

        public bool UpdateUser(User user)
        {
            if (_users.ContainsKey(user.ID))
            {
                _users[user.ID] = user;
                return true;
            }
            return false;
        }

        public bool UsernameExists(string username)
        {
            foreach (var user in _users.Values)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }
    }
}