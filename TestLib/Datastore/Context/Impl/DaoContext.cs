using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;

namespace TestLib.Datastore.Context.Impl
{
    public class DaoContext : IDaoContext, IDisposable
    {
        private string _connectionString;

        private DbConnection _connection;

        private DbTransaction _transaction;


        public DaoContext(string connectionString)
        {
            // TODO コネクション文字列は設定等から取得できるといい
            _connectionString = connectionString;
        }

        #region "IDaoContext 実装"
        public void Open()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            // 例外が発生しなければメンバに格納
            _connection = connection;
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public DbTransaction GetTransaction()
        {
            return _transaction;
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_transaction == null)
            {
                _transaction = _connection.BeginTransaction(isolationLevel);
            }
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public DbCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }
        #endregion

        #region "IDisposable 実装"
        public void Dispose()
        {
            Close();
        }
        #endregion

        private void Close()
        {
            // トランザクションが存在すればロールバック
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            // コネクションクローズ
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
