using System;
using System.Collections.Generic;
using System.Text;
using TestLib.UserManageLogics.Models;

namespace TestLib.UserManageLogics.Logics
{
    public interface IUserFindLogic
    {
        void Add(UserDataModel model);

        IReadOnlyList<UserDataModel> Find(UserFindModel model);
    }
}
