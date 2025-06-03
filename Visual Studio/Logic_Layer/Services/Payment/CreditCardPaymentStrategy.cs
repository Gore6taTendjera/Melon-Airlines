using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Payment
{
    public class CreditCardPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(object paymentDetails)
        {
            if (paymentDetails is CreditCardDTO creditCardDto)
            {
                if (string.IsNullOrWhiteSpace(creditCardDto.CardholderName))
                    return false;

                if (string.IsNullOrWhiteSpace(creditCardDto.CreditCardNumber) || !IsValidCreditCardNumber(creditCardDto.CreditCardNumber))
                    return false;

                if (string.IsNullOrWhiteSpace(creditCardDto.ExpiryDate) || !IsValidExpiryDate(creditCardDto.ExpiryDate))
                    return false;

                if (string.IsNullOrWhiteSpace(creditCardDto.CVV) || !IsValidCVV(creditCardDto.CVV))
                    return false;

                return true;
            }

            return false;
        }

        private bool IsValidCreditCardNumber(string creditCardNumber)
        {
            return !string.IsNullOrWhiteSpace(creditCardNumber) && creditCardNumber.Length >= 12;
        }

        private bool IsValidExpiryDate(string expiryDate)
        {
            return !string.IsNullOrWhiteSpace(expiryDate) && Regex.IsMatch(expiryDate, @"\d{2}/\d{2}");
        }

        private bool IsValidCVV(string cvv)
        {
            return !string.IsNullOrWhiteSpace(cvv) && (cvv.Length == 3 || cvv.Length == 4);
        }
    }
}
