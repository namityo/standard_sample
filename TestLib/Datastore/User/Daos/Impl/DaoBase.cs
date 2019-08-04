using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Datastore.Context;

namespace TestLib.Datastore.User.Daos.Impl
{
    class DaoBase
    {
        protected IDaoContext DaoContext { get; private set; }

        public DaoBase(IDaoContext context)
        {
            this.DaoContext = context;
        }
    }
}
