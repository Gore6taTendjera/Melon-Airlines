using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Payment
{
    public interface IPaymentService
    {
        bool ProcessPayPalPayment(PayPalDTO paypalDto);
        bool ProcessCreditCardPayment(CreditCardDTO creditCardDto);
    }
}
