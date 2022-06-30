
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
        private MakePaymentRequest _testRequest;
        private Account _account;
        Mock<IDataStore<Account>> _mockDataStore;
        Mock<IPaymentValidator> _mockValidator;
        Mock<IPaymentProcessorFactory> _mockPaymentProcessorFactory;
        Mock<IPaymentProcessor> _mockPaymentProcessor;

        private PaymentService _service;
        private string _testAccountId = "testAcc123";

        [TestInitialize()]
        public void TestInitialize()
        {
            _testRequest = new MakePaymentRequest()
            {
                DebtorAccountNumber = _testAccountId,
                Amount = 1,
                PaymentScheme = PaymentScheme.Bacs
            };

            _account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 1000,
                Status = AccountStatus.Disabled
            };

            _mockDataStore = new Mock<IDataStore<Account>>();
            _mockPaymentProcessor = new Mock<IPaymentProcessor>();
            _mockPaymentProcessorFactory = new Mock<IPaymentProcessorFactory>();
            _mockValidator = new Mock<IPaymentValidator>();

            //default setups
            _mockDataStore.Setup(s => s.TryGet(_testAccountId, out _account)).Returns(true);
            _mockPaymentProcessorFactory.Setup(f => f.GetProcessor(It.IsAny<PaymentScheme>())).Returns(_mockPaymentProcessor.Object);
            _mockValidator.Setup(v => v.Validate(_account, It.IsAny<PaymentScheme>())).Returns(true);
            _mockPaymentProcessor.Setup(p => p.ProcessPayment(_account, It.IsAny<decimal>())).Returns(true);

            _service = new PaymentService(_mockDataStore.Object, _mockValidator.Object, _mockPaymentProcessorFactory.Object);

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
        public void Can_MakePayment()
        {

            Assert.AreEqual(MakePaymentResult.Succeeded, _service.MakePayment(_testRequest));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out _account), Times.Once());
            _mockPaymentProcessor.Verify(p => p.ProcessPayment(_account, It.IsAny<decimal>()), Times.Once());
            _mockPaymentProcessorFactory.Verify(f => f.GetProcessor(It.IsAny<PaymentScheme>()), Times.Once());
            _mockValidator.Verify(v=> v.Validate(_account, It.IsAny<PaymentScheme>()), Times.Once());
            _mockDataStore.Verify(d => d.Update(_account), Times.Once());

        }

        [TestMethod()]
        public void Will_Fail_MakePayment_To_Non_Exists_Account()
        {
            _mockDataStore.Reset();
            _mockDataStore.Setup(s => s.TryGet(_testAccountId, out _account)).Returns(false);

            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(new MakePaymentRequest() { }));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out _account), Times.Once());
            _mockPaymentProcessorFactory.Verify(f => f.GetProcessor(It.IsAny<PaymentScheme>()), Times.Never());
            _mockValidator.Verify(v => v.Validate(_account, It.IsAny<PaymentScheme>()), Times.Never());
            _mockPaymentProcessor.Verify(p => p.ProcessPayment(_account, It.IsAny<decimal>()), Times.Never());
            _mockDataStore.Verify(d => d.Update(_account), Times.Never());
        }


        [TestMethod()]
        public void Will_Fail_MakePayment_On_Exception_Getting_Account()
        {
            _mockDataStore.Setup(d => d.TryGet(It.IsAny<string>(), out _account)).Throws(() => new Exception());

            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(_testRequest));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out _account), Times.Once());

            _mockPaymentProcessorFactory.Verify(f => f.GetProcessor(It.IsAny<PaymentScheme>()), Times.Never());
            _mockValidator.Verify(v => v.Validate(_account, It.IsAny<PaymentScheme>()), Times.Never());
            _mockPaymentProcessor.Verify(p => p.ProcessPayment(_account, It.IsAny<decimal>()), Times.Never());
            _mockDataStore.Verify(d => d.Update(_account), Times.Never());
        }


        [TestMethod()]
        public void Will_Fail_MakePayment_On_Exception_When_Updating_Account()
        {
            _mockDataStore.Setup(s => s.Update(_account)).Throws(() => new Exception());

            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(_testRequest));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out _account), Times.Once());
            _mockPaymentProcessorFactory.Verify(f => f.GetProcessor(It.IsAny<PaymentScheme>()), Times.Once());
            _mockValidator.Verify(v => v.Validate(_account, It.IsAny<PaymentScheme>()), Times.Once());
            _mockPaymentProcessor.Verify(p => p.ProcessPayment(_account, It.IsAny<decimal>()), Times.Once());
            _mockDataStore.Verify(d => d.Update(_account), Times.Once());
        }

        [TestMethod()]
        public void Will_Fail_MakePayment_On_PaymentScheme_Validation_Fail()
        {
            _mockValidator.Reset();
            _mockValidator.Setup(v => v.Validate(_account, It.IsAny<PaymentScheme>())).Returns(false);
            
            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(_testRequest));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out _account), Times.Once());
            _mockValidator.Verify(v => v.Validate(_account, It.IsAny<PaymentScheme>()), Times.Once());
            _mockPaymentProcessorFactory.Verify(f => f.GetProcessor(It.IsAny<PaymentScheme>()), Times.Never());
            _mockPaymentProcessor.Verify(p => p.ProcessPayment(_account, It.IsAny<decimal>()), Times.Never());
            _mockDataStore.Verify(d => d.Update(_account), Times.Never());
        }

        [TestMethod()]
        public void Will_Fail_MakePayment_On_GetPaymentProcessor_Exception()
        {
            _mockPaymentProcessorFactory.Reset();
            _mockPaymentProcessorFactory.Setup(f => f.GetProcessor(It.IsAny<PaymentScheme>())).Throws(new NotImplementedException());

            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(_testRequest));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out _account), Times.Once());
 
            _mockValidator.Verify(v => v.Validate(_account, It.IsAny<PaymentScheme>()), Times.Once());
            _mockPaymentProcessorFactory.Verify(f => f.GetProcessor(It.IsAny<PaymentScheme>()), Times.Once());
            _mockPaymentProcessor.Verify(p => p.ProcessPayment(_account, It.IsAny<decimal>()), Times.Never());
            _mockDataStore.Verify(d => d.Update(_account), Times.Never());
        }

        [TestMethod()]
        public void Will_Fail_MakePayment_On_PaymentProcessor_Fail()
        {
            _mockPaymentProcessor.Setup(p => p.ProcessPayment(_account, It.IsAny<decimal>())).Returns(false);

            Assert.AreEqual(MakePaymentResult.Failed, _service.MakePayment(_testRequest));

            _mockDataStore.Verify(d => d.TryGet(It.IsAny<string>(), out _account), Times.Once());
            _mockValidator.Verify(v => v.Validate(_account, It.IsAny<PaymentScheme>()), Times.Once());
            _mockPaymentProcessorFactory.Verify(f => f.GetProcessor(It.IsAny<PaymentScheme>()), Times.Once());
            _mockPaymentProcessor.Verify(p => p.ProcessPayment(_account, It.IsAny<decimal>()), Times.Once());
            _mockDataStore.Verify(d => d.Update(_account), Times.Never());
        }

    }
}
