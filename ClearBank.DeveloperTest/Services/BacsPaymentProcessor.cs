using ClearBank.DeveloperTest.Types;

using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Services
{
    internal class BacsPaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(Account account, decimal amount)
        {
            account.Balance -= amount;
            return true;
        }
    }
}
