
namespace ClearBank.DeveloperTest.Types
{
    internal static class AccountExtensions
    {
        internal static bool ProcessPayment(this Account account, PaymentScheme scheme, decimal amount)
        {
            if (!account.AllowedPaymentScheme(scheme))
                return false;

            return scheme switch
            {
                PaymentScheme.Bacs => account.ProcessBacs(amount),

                PaymentScheme.FasterPayments => account.ProcessFasterPayments(amount),

                PaymentScheme.Chaps => account.ProcessChaps(amount),

                _ => throw new System.NotImplementedException(),
            };
        }


        internal static bool AllowedPaymentScheme(this Account account, PaymentScheme scheme) => scheme switch
        {
            PaymentScheme.Bacs => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs),
            PaymentScheme.Chaps => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps),
            PaymentScheme.FasterPayments => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments),
            _ => throw new System.NotImplementedException(),
        };

        internal static bool ProcessBacs(this Account account, decimal amount )
        {
            account.Balance -= amount;
            return true;
        }

        internal static bool ProcessFasterPayments(this Account account, decimal amount)
        {
            if (account.Balance < amount)
            {
                return false;
            }
            account.Balance -= amount;
            return true;
        }

        internal static bool ProcessChaps(this Account account, decimal amount)
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
