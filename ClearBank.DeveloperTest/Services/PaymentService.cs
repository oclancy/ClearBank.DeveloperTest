using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentValidator _paymentValidator;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;
        private readonly IDataStore<Account> _dataStore;

        public PaymentService(IDataStore<Account> dataStore, 
                              IPaymentValidator paymentValidator, 
                              IPaymentProcessorFactory paymentProcessorFactory)
        {
            _dataStore = dataStore;
            _paymentValidator = paymentValidator;
            _paymentProcessorFactory = paymentProcessorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            if(request == null) return MakePaymentResult.Failed;

#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                if (!_dataStore.TryGet(request.DebtorAccountNumber, out var account))
                    return MakePaymentResult.Failed;

                // valid transaction type?
                if( !_paymentValidator.Validate(account, request.PaymentScheme))
                    return MakePaymentResult.Failed;

                var paymentProcessor = _paymentProcessorFactory.GetProcessor(request.PaymentScheme);

                if (!paymentProcessor.ProcessPayment(account, request.Amount))
                    return MakePaymentResult.Failed;

                _dataStore.Update(account);
                return MakePaymentResult.Succeeded;
            }
            catch (Exception)
            {
                // could throw if calling service can do something sensible
                // but for now...
                // log exception
                return MakePaymentResult.Failed;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
