using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DTOs
{
	public class UserProfileDetailsDTO
	{
		[ValidateNever]
		public int ID { get; set; }

		[StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
		public string? Name { get; set; }

		[StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters.")]
		public string? MiddleName { get; set; }

		[StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
		public string? Surname { get; set; }

		public Gender? Gender { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? BirthDate { get; set; }

		[StringLength(100, ErrorMessage = "Birthplace cannot exceed 100 characters.")]
		public string? BirthPlace { get; set; }

		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string? Email { get; set; }

		public Nationality? Nationality { get; set; }
	}

}
