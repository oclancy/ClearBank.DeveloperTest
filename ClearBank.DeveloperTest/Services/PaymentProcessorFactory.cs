using ClearBank.DeveloperTest.Types;

using System;
using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Services
{
    internal class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        private BacsPaymentProcessor _bacsPaymentProcessor = new BacsPaymentProcessor();
        private FasterPaymentProcessor _fasterPaymentProcessor = new FasterPaymentProcessor();
        private ChapsPaymentProcessor _chapsPaymentProcessor = new ChapsPaymentProcessor();

        private readonly Dictionary<PaymentScheme, IPaymentProcessor> _processorLookup;

        public PaymentProcessorFactory()
        {
            _processorLookup = new Dictionary<PaymentScheme, IPaymentProcessor>()
            {
                { PaymentScheme.Bacs, _bacsPaymentProcessor},
                { PaymentScheme.Chaps, _chapsPaymentProcessor},
                { PaymentScheme.FasterPayments, _fasterPaymentProcessor},
            };
        }

        public IPaymentProcessor GetProcessor(PaymentScheme scheme) 
        {
            if (!_processorLookup.ContainsKey(scheme))
                throw new NotImplementedException();
            
            return _processorLookup[scheme];
        }
    }
}
