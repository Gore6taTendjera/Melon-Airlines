using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class DocumentDTO
    {
        [StringLength(20, ErrorMessage = "Document number cannot exceed 20 characters.")]
        public string? DocumentNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfIssue { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfExpire { get; set; }

        [ValidateNever]
        public int UserID { get; set; }
    }
}
