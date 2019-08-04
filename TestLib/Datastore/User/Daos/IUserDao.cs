using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Datastore.User.Models;

namespace TestLib.Datastore.User.Daos
{
    public interface IUserDao
    {
        void AddUser(Models.UserModel user);

        UserModel GetUser(long id);

        IReadOnlyList<Models.UserModel> GetUsers(UserFindParameter model);

        long GetCount();
    }
}
