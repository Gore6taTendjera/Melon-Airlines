using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        public bool ProcessPayPalPayment(PayPalDTO paypalDto)
        {
            IPaymentStrategy strategy = PaymentStrategyFactory.CreatePayment(paypalDto);
            return strategy.ProcessPayment(paypalDto);
        }

        public bool ProcessCreditCardPayment(CreditCardDTO creditCardDto)
        {
            IPaymentStrategy strategy = PaymentStrategyFactory.CreatePayment(creditCardDto);
            return strategy.ProcessPayment(creditCardDto);
        }
    }
}
