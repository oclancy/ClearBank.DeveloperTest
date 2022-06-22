
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestClass]
    public class When_Using_DataStore
    {

        [TestInitialize()]
        public void TestInitialize()
        {
            
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            
        }

        [TestMethod()]
        public void Can_Get_Primary_Account_DataStore()
        {
            var provider = new DataStoreProvider(DataStoreType.Primary);

            Assert.IsInstanceOfType( provider.GetAccountDataStore(), typeof(AccountDataStore));
        }

        [TestMethod()]
        public void Can_Get_Backup_Account_DataStore()
        {
            var provider = new DataStoreProvider(DataStoreType.Backup);

            Assert.IsInstanceOfType(provider.GetAccountDataStore(), typeof(BackupAccountDataStore));
        }

    }
}
