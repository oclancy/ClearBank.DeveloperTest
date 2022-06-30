using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_PaymentProcessorFactory
    {
        private PaymentProcessorFactory _paymentProcessorFactory;


        [TestInitialize()]
        public void TestInitialize()
        {
            _paymentProcessorFactory = new PaymentProcessorFactory();
        }

        [TestCleanup()]
        public void TestCleanup()
        {

        }

        [TestMethod()]
        public void Can_GetProcessor_For_Chaps_Payment_Request()
        {
            Assert.IsInstanceOfType(_paymentProcessorFactory.GetProcessor(PaymentScheme.Chaps), typeof(ChapsPaymentProcessor));
        }


        [TestMethod()]
        public void Can_GetProcessor_For_Faster_Payment_Request()
        {
            Assert.IsInstanceOfType(_paymentProcessorFactory.GetProcessor(PaymentScheme.FasterPayments), typeof(FasterPaymentProcessor));
        }


        [TestMethod()]
        public void Can_GetProcessor_For_Bacs_Payment_Request()
        {
            Assert.IsInstanceOfType(_paymentProcessorFactory.GetProcessor(PaymentScheme.Bacs), typeof(BacsPaymentProcessor));
        }


        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void Will_Throw_Exception_On_Unknown_PaymentScheme()
        {
            Assert.IsInstanceOfType(_paymentProcessorFactory.GetProcessor((PaymentScheme)100), typeof(object));
        }
    }
}