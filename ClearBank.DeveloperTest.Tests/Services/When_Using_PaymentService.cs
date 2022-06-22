
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_PaymentService
    {

        Mock<IDataStore<Account>> _mockDataStore;
        private PaymentService _service;
        private string _testAccountId = "testAcc123";

        [TestInitialize()]
        public void TestInitialize()
        {
            _mockDataStore = new Mock<IDataStore<Account>>();
            _service = new PaymentService(_mockDataStore.Object);
        }
        [TestCleanup()]
        public void TestCleanup()
        {

        }

        [TestMethod()]
        public void It_Implements_Behaviours()
        {
            Assert.IsInstanceOfType(_service, typeof(IPaymentService));
        }

        [TestMethod()]
        public void Will_Fail_MakePayment_To_Non_Exists_Account()
        {
            var account = new Account();
            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(new MakePaymentRequest() { }));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out account), Times.Once());
            _mockDataStore.Verify(d => d.Update(account), Times.Never());
        }

        [TestMethod()]
        public void Can_MakePayment_To_Account_Bacs()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 1000,
                Status = AccountStatus.Disabled
            };

            _mockDataStore.Setup(s => s.TryGet(_testAccountId, out account)).Returns(true);
            var request = new MakePaymentRequest()
            {
                DebtorAccountNumber = _testAccountId,
                Amount = 1,
                PaymentScheme = PaymentScheme.Bacs
            };


            Assert.AreEqual(MakePaymentResult.Succeeded, _service.MakePayment(request));

            Assert.AreEqual(1000 - request.Amount, account.Balance);

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out account), Times.Once());
            _mockDataStore.Verify(d => d.Update(account), Times.Once());

        }

        [TestMethod()]
        public void Will_Fail_MakePayment_On_Exception_Getting_Account()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 1000,
                Status = AccountStatus.Disabled
            };

            var request = new MakePaymentRequest()
            {
                DebtorAccountNumber = _testAccountId,
                Amount = 1,
                PaymentScheme = PaymentScheme.Bacs
            };

            _mockDataStore.Setup(d => d.TryGet(It.IsAny<string>(), out account)).Throws(() => new Exception());

            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(request));
            Assert.AreEqual(1000, account.Balance);

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out account), Times.Once());
            _mockDataStore.Verify(d => d.Update(account), Times.Never());
        }

        [TestMethod()]
        public void Can_MakePayment_To_Account_Chaps()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Balance = 1000,
                Status = AccountStatus.Live
            };

            _mockDataStore.Setup(s => s.TryGet(_testAccountId, out account)).Returns(true);
            var request = new MakePaymentRequest()
            {
                DebtorAccountNumber = _testAccountId,
                Amount = 1,
                PaymentScheme = PaymentScheme.Chaps
            };


            Assert.AreEqual(MakePaymentResult.Succeeded, _service.MakePayment(request));

            Assert.AreEqual(1000 - request.Amount, account.Balance);

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out account), Times.Once());
            _mockDataStore.Verify(d => d.Update(account), Times.Once());

        }

        [TestMethod()]
        public void Can_MakePayment_To_Account_Faster_Payments()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 1000,
                Status = AccountStatus.Disabled
            };

            _mockDataStore.Setup(s => s.TryGet(_testAccountId, out account)).Returns(true);
            var request = new MakePaymentRequest()
            {
                DebtorAccountNumber = _testAccountId,
                Amount = 1,
                PaymentScheme = PaymentScheme.FasterPayments
            };


            Assert.AreEqual(MakePaymentResult.Succeeded, _service.MakePayment(request));

            Assert.AreEqual(1000 - request.Amount, account.Balance);

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out account), Times.Once());
            _mockDataStore.Verify(d => d.Update(account), Times.Once());

        }

        [TestMethod()]
        public void Will_Fail_MakePayment_On_Exception_When_Updating_Account()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 1000,
                Status = AccountStatus.Disabled
            };

            _mockDataStore.Setup(s => s.TryGet(_testAccountId, out account)).Returns(true);
            _mockDataStore.Setup(s => s.Update(account)).Throws(() => new Exception());

            var request = new MakePaymentRequest()
            {
                DebtorAccountNumber = _testAccountId,
                Amount = 1,
                PaymentScheme = PaymentScheme.Bacs
            };


            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(request));

            Assert.AreEqual(1000 - request.Amount, account.Balance);

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out account), Times.Once());
            _mockDataStore.Verify(d => d.Update(account), Times.Once());

        }

    }
}
