using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Datastore.Context;
using TestLib.Datastore.User.Daos;
using TestLib.Datastore.User.Daos.Impl;

namespace TestLib.Datastore.User.Factory.Impl
{
    public class UserStoreFactory : IUserStoreFactory
    {
        public IUserDao CreateUserDao(IDaoContext context)
        {
            return new UserDao(context);
        }
    }
}
