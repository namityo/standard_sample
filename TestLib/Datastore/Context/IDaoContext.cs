using System;
using System.Data;
using System.Data.Common;

namespace TestLib.Datastore.Context
{
    public interface IDaoContext : IDisposable
    {
        void Open();

        DbConnection GetConnection();

        DbTransaction GetTransaction();

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void Commit();

        DbCommand CreateCommand();
    }
}
