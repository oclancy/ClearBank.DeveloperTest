using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_FasterPaymentProcessor
    {
        readonly decimal _startBalance = 1000m;
        FasterPaymentProcessor _processor;

        [TestInitialize()]
        public void TestInitialize()
        {
            _processor = new FasterPaymentProcessor();

        }
        [TestCleanup()]
        public void TestCleanup()
        {

        }

        [TestMethod()]
        public void Can_Process_Faster_Request()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsTrue(_processor.ProcessPayment(testAccount, 1m));

            Assert.AreEqual(_startBalance - 1m, testAccount.Balance);
        }
        
        [TestMethod()]
        public void Will_Not_Process_FasterPayments_Request_If_Insufficient_Balance()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsFalse(_processor.ProcessPayment(testAccount, 1001m));

            Assert.AreEqual(_startBalance, testAccount.Balance);
        }

    }
}
