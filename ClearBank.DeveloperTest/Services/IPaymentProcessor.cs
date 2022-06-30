using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(Account account, decimal amount);
    }
}
