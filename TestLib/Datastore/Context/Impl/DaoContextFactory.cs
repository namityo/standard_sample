using System;
using System.Collections.Generic;
using System.Text;

namespace TestLib.Datastore.Context.Impl
{
    class DaoContextFactory : IDaoContextFactory
    {
        public IDaoContext CreateContext()
        {
            // TODO 接続文字列は設定等から取得する
            var connectionString = Environement.Variables.GetInstance().DataStoreConnectionString;

            // オープンまでしちゃう
            var dao = new DaoContext(connectionString);
            dao.Open();

            return dao;
        }
    }
}
