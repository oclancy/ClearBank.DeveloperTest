using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    internal class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        private BacsPaymentProcessor _bacsPaymentProcessor = new BacsPaymentProcessor();
        private FasterPaymentProcessor _fasterPaymentProcessor = new FasterPaymentProcessor();
        private ChapsPaymentProcessor _chapsPaymentProcessor = new ChapsPaymentProcessor();

        public  IPaymentProcessor GetProcessor(PaymentScheme scheme) =>
        scheme switch
            {
                PaymentScheme.Bacs => _bacsPaymentProcessor,

                PaymentScheme.FasterPayments => _fasterPaymentProcessor, 

                PaymentScheme.Chaps => _chapsPaymentProcessor,

                _ => throw new System.NotImplementedException(),
            };
}
}
