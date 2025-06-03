using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
	public class UserRegisterDTO
	{
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Username is required.")]
		[StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
		public string Password { get; set; }
	}
}
