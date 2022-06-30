using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    internal class FasterPaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(Account account, decimal amount)
        {
            if (account.Balance < amount)
            {
                return false;
            }
            account.Balance -= amount;
            return true;
        }
    }
}
