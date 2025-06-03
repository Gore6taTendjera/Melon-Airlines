using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CreditCardDTO
    {
        [Required(ErrorMessage = "Cardholder Name is required")]
        [StringLength(100, ErrorMessage = "Cardholder Name cannot exceed 100 characters")]
        public string CardholderName { get; set; }

        [Required(ErrorMessage = "Credit Card Number is required")]
        [CreditCard(ErrorMessage = "Invalid Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Expiry Date is required")]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "Expiry Date must be in MM/YY format")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        [RegularExpression(@"\d{3,4}", ErrorMessage = "Invalid CVV")]
        public string CVV { get; set; }
    }

}
