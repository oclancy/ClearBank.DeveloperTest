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
        public bool TryGet(string id, out Account value)
        {
            // Access backup data base to retrieve account, code removed for brevity and assuming success
            value = new Account();
            return true;
        }

        public void Update(Account item)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
