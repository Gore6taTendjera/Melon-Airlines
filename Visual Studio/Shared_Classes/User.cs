using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Shared_Classes
{
    public class User
    {
        // nulltypes because the user can create account without need to input every property
        // and the database saves Username & Password only.
        // Later when creating a document it will ask the user to type the remaining props and save to database.
        public int ID { get; private set; } // database operation
        public string? Name { get; private set; }
        public string? MiddleName { get; private set; }
        public string? Surname { get; private set; }
        public Gender? Gender { get; private set; }
        public DateOnly? BirthDate { get; private set; }
        public string? BirthPlace { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public Nationality? Nationality { get; private set; }
        public UserType UserType { get; private set; }


        // registerDTO
        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.UserType = 0;
        }

		// create User winforms all props
		public User(string name, string middleName, string surname, Gender gender, DateOnly birthDate, string birthPlace,
	        string username, string password, string email, Nationality nationality, UserType userType)
		{
			this.Name = name;
			this.MiddleName = middleName;
			this.Surname = surname;
			this.Gender = gender;
			this.BirthDate = birthDate;
			this.BirthPlace = birthPlace;
			this.Username = username;
			this.Password = password;
			this.Email = email;
			this.Nationality = nationality;
			this.UserType = userType;
		}


        // database
        public User(int id, string name, string middleName, string surname, Gender gender, DateOnly birthDate,
                string birthPlace, string username, string password, string email, Nationality nationality, UserType userType)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be a positive integer.", nameof(id));


            this.ID = id;
            this.Name = name;
            this.MiddleName = middleName;
            this.Surname = surname;
            this.Gender = gender;
            this.BirthDate = birthDate;
            this.BirthPlace = birthPlace;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Nationality = nationality;
            this.UserType = userType;
        }


        // update userDetails used in WEBSITE in User Page
        public User(int id, string? name, string? middleName, string? surname, Gender? gender,
                 DateOnly? birthDate, string? birthPlace, string email, Nationality? nationality)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be a positive integer.", nameof(id));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email must not be null or empty.", nameof(email));


            this.ID = id;
            this.Name = name;
            this.MiddleName = middleName;
            this.Surname = surname;
            this.Gender = gender;
            this.BirthDate = birthDate;
            this.BirthPlace = birthPlace;
            this.Email = email;
            this.Nationality = nationality;
        }


        public string GetInfo()
        {
            return $"ID: {ID}, Name: {Name}, MiddleName: {MiddleName}, Surname: {Surname},  Gender: {Gender}, BirthDate: {BirthDate}," +
                $"BirthPlace: {BirthPlace}, Username: {Username}, Password: {Password}, Email: {Email}, Nationality: {Nationality}";
        }

 

    }
}
