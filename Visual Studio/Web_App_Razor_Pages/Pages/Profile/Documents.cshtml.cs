using DTOs;
using Enums;
using Logic_Layer.Interface.LL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared_Classes;
using System.ComponentModel.DataAnnotations;

namespace Web_App_Razor_Pages.Pages.Profile
{
	public class DocumentsPageModel : PageModel
	{
		private readonly IIDService _ID;
		private readonly IPassportService _passport;
		private readonly IUserAccountService _userAccountService;
		private readonly IImageService _imageService;

		[BindProperty]
		public UserProfileDetailsDTO UserProfileDetails { get; set; }
		[BindProperty]
		public DocumentDTO documentPassportDetails { get; set; }
		[BindProperty]
		public DocumentDTO documentIDDetails { get; set; }

		[BindProperty]
		public ImageDTO? ImageDTO { get; set; }

		[BindProperty]
		public IFormFile? Image { get; set; }
		// there is a very big problem with this, [?] making it null will bypass the model validation
		// even when y have put the [ValidateNever] it will ALWAYS say that the model is invalid IN ANY CASE!!!
		// also will say that you you never uploaded a file on the html validation.


		public DocumentsPageModel(IIDService id, IPassportService passport, IUserAccountService userAccountService, IImageService imageUploadService)
		{
			_ID = id;
			_passport = passport;
			_userAccountService = userAccountService;
			_imageService = imageUploadService;
		}


		public IActionResult OnGet()
		{
			if (User.Identity.IsAuthenticated && User.IsInRole(nameof(UserType.Normal)))
			{
				if (!HttpContext.Session.Keys.Any())
				{
					return LogoutUser();
				}

				LoadData();

				return Page();
			}
			else
			{
				return RedirectToPage("/Login");
			}
		}

		private void LoadData()
		{
			int userId = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));

			documentPassportDetails = _passport.GetPassportByUserIDDTO(userId);
			documentIDDetails = _ID.GetIDByUserIDDTO(userId);

			UserProfileDetails = _userAccountService.GetUserProfileDetails(userId);
			SetHasDocuments();

			LoadImage();

		}

		private void LoadImage()
		{
			int userId = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));

			ImageDTO = _imageService.GetImageByUserIDDTO(userId);
		}


		private IActionResult LogoutUser()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Login");
		}


		public IActionResult OnPostUpdateUser()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			UserProfileDetails.ID = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			_userAccountService.UpdateUserDetails(UserProfileDetails);

			return RedirectToPage();
		}



		public IActionResult OnPostUpdatePassport()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			documentPassportDetails.UserID = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			_passport.CreatePassportDTO(documentPassportDetails);

			HttpContext.Session.SetString("HasDocuments", true.ToString());

			return RedirectToPage();
		}

		public IActionResult OnPostUpdateID()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			documentIDDetails.UserID = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			_ID.CreateIDDTO(documentIDDetails);

			HttpContext.Session.SetString("HasDocuments", true.ToString());

			return RedirectToPage();
		}

		private void SetHasDocuments()
		{
			bool pass = MissingPassportDetails();
			bool id = MissingIdDetails();
			if (!pass && !id)
			{
				HttpContext.Session.SetString("HasDocuments", true.ToString());
			}
			else
			{
				HttpContext.Session.SetString("HasDocuments", false.ToString());
			}
		}

		public bool AnyUserDetails()
		{
			return UserProfileDetails == null ||
				UserProfileDetails.Name == null ||
				UserProfileDetails.MiddleName == null ||
				UserProfileDetails.Surname == null ||
				UserProfileDetails.Gender == null ||
				UserProfileDetails.Gender == Gender.NONE ||
				UserProfileDetails.BirthPlace == null ||
				UserProfileDetails.BirthDate == null ||
				UserProfileDetails.BirthDate == DateTime.MinValue ||
				UserProfileDetails.Nationality == null;
		}

		public bool MissingPassportDetails()
		{
			int userId = UserProfileDetails.ID = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			return !_passport.HasPassport(userId);
		}

		public bool MissingIdDetails()
		{
			int userId = UserProfileDetails.ID = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			return !_ID.HasID(userId);
		}


		public IActionResult OnPostUploadImage(IFormFile imageFile)
		{
			if (imageFile == null || imageFile.Length == 0)
			{
				ModelState.AddModelError(nameof(Image), "Please upload a valid image.");
				LoadData();
				return Page();
			}

			// Check the MIME type of the uploaded file
			var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/tiff" };
			if (!validImageTypes.Contains(imageFile.ContentType))
			{
				ModelState.AddModelError(nameof(Image), "Please upload an image file (jpeg, png, gif, bmp, tiff).");
				LoadData();
				return Page();
			}

			int id = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));

			using (var memoryStream = new MemoryStream())
			{
				imageFile.CopyTo(memoryStream);
				var imageDTO = new ImageDTO
				{
					UserId = id,
					Data = memoryStream.ToArray(),
					ContentType = imageFile.ContentType
				};

				_imageService.UploadImage(imageDTO);
			}
			LoadData();
			return Page();
		}


		public void OnPostDeleteImage()
		{
			int id = Convert.ToInt32(HttpContext.Session.GetString(nameof(UserLoginDTO.ID)));
			_imageService.DeleteImageByUserID(id);
			LoadData();
		}

	}
}
