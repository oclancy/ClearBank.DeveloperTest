using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStore : IDataStore<Account>
    {
        public bool TryGet(string id, out Account value)
        {
            // Access database to retrieve account, code removed for brevity (assumed success for now)
            value = new Account();
            return true;
        }

        public void Update(Account item)
        {
            // Update account in database, code removed for brevity
        }
    }
}
