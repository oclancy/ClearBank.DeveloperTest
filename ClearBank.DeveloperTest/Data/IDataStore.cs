using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Data
{

    /// <summary>
    /// Behaviour of a datastore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        bool TryGet(string Id, out T value);

        void Update(T item);
    }
}
