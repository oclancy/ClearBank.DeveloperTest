using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStore : IDataStore<Account>
    {
        public bool TryGet(string accountNumber, out Account account)
        {
            // Access database to retrieve account, code removed for brevity (assumed success for now)
            account = new Account();
            return true;
        }

        public void Update(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
