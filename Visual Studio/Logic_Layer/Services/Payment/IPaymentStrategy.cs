using DTOs;

namespace Logic_Layer.Services.Payment
{
    public interface IPaymentStrategy
    {
        bool ProcessPayment(object paymentDetails);
    }
}