using ClearBank.DeveloperTest.Types;

using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Services
{
    internal class PaymentValidator : IPaymentValidator
    {
        public bool Validate(Account account, PaymentScheme scheme)

            => scheme switch
            {
                PaymentScheme.Bacs => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs),
                PaymentScheme.Chaps => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps),
                PaymentScheme.FasterPayments => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments),
                _ => throw new System.NotImplementedException(),
            };


    }
}
