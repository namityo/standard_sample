using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Datastore.Context;
using TestLib.Datastore.User.Daos;

namespace TestLib.Datastore.User.Factory
{
    public interface IUserStoreFactory
    {
        IUserDao CreateUserDao(IDaoContext context);
    }
}
