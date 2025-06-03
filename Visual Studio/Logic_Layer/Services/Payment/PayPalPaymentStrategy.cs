using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services.Payment
{
    public class PayPalPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(object paymentDetails)
        {
            if (paymentDetails is PayPalDTO paypalDto)
            {
                if (!string.IsNullOrWhiteSpace(paypalDto.Email))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
