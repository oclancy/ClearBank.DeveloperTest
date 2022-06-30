using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentService(IDataStore<Account> dataStore)
        {
            DataStore = dataStore;
        }

        private IDataStore<Account> DataStore { get; }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            if(request == null) return MakePaymentResult.Failed;

            try
            {
                // valid transaction type?
                if (!DataStore.TryGet(request.DebtorAccountNumber, out var account))
                    return MakePaymentResult.Failed;

                if (!account.ProcessPayment(request.PaymentScheme, request.Amount))
                    return MakePaymentResult.Failed;

                DataStore.Update(account);
                return MakePaymentResult.Succeeded;
            }
            catch (Exception)
            {
                // could throw if calling service can do something sensible
                // but for now...
                // log exception
                return MakePaymentResult.Failed;
            }
        }
    }
}
