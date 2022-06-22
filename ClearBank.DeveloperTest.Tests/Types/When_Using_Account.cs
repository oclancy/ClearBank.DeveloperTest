
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_Account
    {
        decimal startBalance = 1000m;

        [TestInitialize()]
        public void TestInitialize()
        {
            
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
                Balance = startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            Assert.IsTrue(testAccount.ProcessPayment(PaymentScheme.Bacs, 1m));

            Assert.AreEqual(startBalance - 1m, testAccount.Balance);
           
        }

        [TestMethod()]
        public void Will_Not_Process_Bacs_Request_If_Not_Allowed()
        {
            var testAccount = new Account()
            {
                Balance = startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsFalse(testAccount.ProcessPayment(PaymentScheme.Bacs, 1m));

            Assert.AreEqual(startBalance, testAccount.Balance);

        }


        [TestMethod()]
        public void Can_Process_Chaps_Request()
        {
            var testAccount = new Account()
            {
                Balance = startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            Assert.IsTrue(testAccount.ProcessPayment(PaymentScheme.Chaps, 1m));

            Assert.AreEqual(startBalance - 1m, testAccount.Balance);
        }

        [TestMethod()]
        public void Will_Not_Process_Chaps_Request_If_Not_Allowed()
        {
            var testAccount = new Account()
            {
                Balance = startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsFalse(testAccount.ProcessPayment(PaymentScheme.Chaps, 1m));

            Assert.AreEqual(startBalance, testAccount.Balance);

        }

        [TestMethod()]
        public void Will_Not_Process_Chaps_Request_If_Not_Status_Live()
        {
            var testAccount = new Account()
            {
                Balance = startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled
            };

            Assert.IsFalse(testAccount.ProcessPayment(PaymentScheme.Chaps, 1m));

            Assert.AreEqual(startBalance, testAccount.Balance);
        }


        [TestMethod()]
        public void Can_Process_FasterPayments_Request()
        {
            var testAccount = new Account()
            {
                Balance = startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsTrue(testAccount.ProcessPayment(PaymentScheme.FasterPayments, 1m));

            Assert.AreEqual(startBalance - 1m, testAccount.Balance);
        }


        [TestMethod()]
        public void Will_Not_Process_FasterPayments_Request_If_Not_Allowed()
        {
            var testAccount = new Account()
            {
                Balance = 1000,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            Assert.IsFalse(testAccount.ProcessPayment(PaymentScheme.FasterPayments, 1m));

            Assert.AreEqual(startBalance, testAccount.Balance);

        }

        [TestMethod()]
        public void Will_Not__Process_FasterPayments_Request_If_Insufficient_Balance()
        {
            var testAccount = new Account()
            {
                Balance = startBalance,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };

            Assert.IsFalse(testAccount.ProcessPayment(PaymentScheme.FasterPayments, 1001m));

            Assert.AreEqual(startBalance, testAccount.Balance);
        }
    }
}
