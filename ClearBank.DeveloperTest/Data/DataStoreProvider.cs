using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Data
{
    /// <summary>
    /// Gets a typed DataStore 
    /// </summary>
    internal class DataStoreProvider
    {
        private readonly DataStoreType dataStoreType;

        public DataStoreProvider(DataStoreType type)
        {
            dataStoreType = type;
        }

        /// <summary>
        /// Get the Accounts DataStore
        /// </summary>
        /// <returns></returns>
        public IDataStore<Account> GetAccountDataStore()
        {
            return dataStoreType switch
            {
                DataStoreType.Backup => new BackupAccountDataStore(),
                _ => new AccountDataStore(),
            };
        }
    }
}
