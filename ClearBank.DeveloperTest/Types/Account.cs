
namespace ClearBank.DeveloperTest.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }

        internal bool ProcessPayment(PaymentScheme scheme, decimal amount)
        {
            if (!AllowedPaymentScheme(scheme))
                return false;

            switch (scheme)
            {
                case PaymentScheme.Bacs:
                    break;

                case PaymentScheme.FasterPayments:
                    if (Balance < amount)
                    {
                        return false;
                    }
                    break;

                case PaymentScheme.Chaps:

                    if (Status != AccountStatus.Live)
                    {
                        return false;
                    }
                    break;
            }

            Balance -= amount;

            return true;
        }


        private bool AllowedPaymentScheme(PaymentScheme scheme) => scheme switch
        {
            PaymentScheme.Bacs => AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs),
            PaymentScheme.Chaps => AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps),
            PaymentScheme.FasterPayments => AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments),
            _ => throw new System.NotImplementedException(),
        };
    }
}
