using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_BacsPaymentProcessor
    {
        readonly decimal _startBalance = 1000m;
        BacsPaymentProcessor _processor;

        [TestInitialize()]
        public void TestInitialize()
        {
            _processor = new BacsPaymentProcessor();
        }

        [TestCleanup()]
        public void TestCleanup()
        {

        }

        [TestMethod()]
        public void Can_Process_Bacs_Request()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            Assert.IsTrue(_processor.ProcessPayment(testAccount, 1m));

            Assert.AreEqual(_startBalance - 1m, testAccount.Balance);

        }
    }
}
