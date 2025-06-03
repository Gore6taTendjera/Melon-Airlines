using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Payment
{
    public class PaymentStrategyFactory
    {
        public static IPaymentStrategy CreatePayment(object obj)
        {
            switch (obj)
            {
                case (CreditCardDTO):
                    return new CreditCardPaymentStrategy();

                case (PayPalDTO):
                    return new PayPalPaymentStrategy();

                default:
                    throw new ArgumentException("Unsupported payment");
            }
        }
    }
}
