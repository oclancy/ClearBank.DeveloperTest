using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Data
{
    /// <summary>
    /// I'm making the assumption that this would actually be somehow functionally different to AccountDataStore in someway and not
    /// just a difference in e.g connection string.
    /// </summary>
    public class BackupAccountDataStore : IDataStore<Account>
    {
        public bool TryGet(string accountNumber, out Account account)
        {
            // Access backup data base to retrieve account, code removed for brevity and assuming success
            account = new Account();
            return true;
        }

        public void Update(Account account)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
