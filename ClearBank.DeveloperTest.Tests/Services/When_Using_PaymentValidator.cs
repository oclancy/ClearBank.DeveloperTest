using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_PaymentValidator
    {
        private readonly decimal _startBalance = 1000m;
        private PaymentValidator _validator;

        [TestInitialize()]
        public void TestInitialize()
        {
            _validator = new PaymentValidator();
        }
        [TestCleanup()]
        public void TestCleanup()
        {

        }


        [TestMethod()]
        public void Can_Validate_Account_For_Chaps_Request()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            Assert.IsTrue(_validator.Validate(testAccount, PaymentScheme.Chaps));
            Assert.IsFalse(_validator.Validate(testAccount, PaymentScheme.Bacs));
            Assert.IsFalse(_validator.Validate(testAccount, PaymentScheme.FasterPayments));
        }

        [TestMethod()]
        public void Can_Validate_Account_For_Bacs_Request()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Status = AccountStatus.Live
            };

            Assert.IsFalse(_validator.Validate(testAccount, PaymentScheme.Chaps));
            Assert.IsTrue(_validator.Validate(testAccount, PaymentScheme.Bacs));
            Assert.IsFalse(_validator.Validate(testAccount, PaymentScheme.FasterPayments));
        }

        [TestMethod()]
        public void Can_Validate_Account_For_FasterPayments_Request()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsFalse(_validator.Validate(testAccount, PaymentScheme.Chaps));
            Assert.IsFalse(_validator.Validate(testAccount, PaymentScheme.Bacs));
            Assert.IsTrue(_validator.Validate(testAccount, PaymentScheme.FasterPayments));
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void Will_Throw_Exception_For_Unknown_PaymentScheme()
        {
            var testAccount = new Account()
            {
                Balance = _startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsTrue(_validator.Validate(testAccount, (PaymentScheme)100));
        }
    }
}
