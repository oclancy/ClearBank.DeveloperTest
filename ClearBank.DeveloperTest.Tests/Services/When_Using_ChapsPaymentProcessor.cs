using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_ChapsPaymentProcessor
    {
        private readonly decimal _startBalance = 1000m;
        private ChapsPaymentProcessor _processor;

        [TestInitialize()]
        public void TestInitialize()
        {
            _processor = new ChapsPaymentProcessor();
        }
        [TestCleanup()]
        public void TestCleanup()
        {

        }

        [TestMethod()]
        public void Can_Process_Chaps_Request()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            Assert.IsTrue(_processor.ProcessPayment(testAccount, 1m));

            Assert.AreEqual(_startBalance - 1m, testAccount.Balance);
        }

        [TestMethod()]
        public void Will_Not_Process_Chaps_Request_If_Not_Status_Live()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled
            };

            Assert.IsFalse(_processor.ProcessPayment(testAccount, 1m));

            Assert.AreEqual(_startBalance, testAccount.Balance);
        }

    }
}
