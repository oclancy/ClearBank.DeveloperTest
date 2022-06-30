using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    internal class ChapsPaymentProcessor : IPaymentProcessor
    {
       public bool ProcessPayment(Account account, decimal amount)
        {
            if (account.Status != AccountStatus.Live)
            {
                return false;
            }
            account.Balance -= amount;
            return true;
        }
    }
}
