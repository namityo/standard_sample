using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TestLib.Datastore.Context;
using TestLib.Datastore.User.Factory;
using TestLib.Datastore.User.Models;
using TestLib.UserManageLogics.Models;

namespace TestLib.UserManageLogics.Logics.Impl
{
    class UserFindLogic : IUserFindLogic
    {
        public IDaoContextFactory DaoContextFactory { get; set; }

        public IUserStoreFactory UserStoreFactory { get; set; }

        #region "IUserFindLogic 実装"
        public void Add(UserDataModel model)
        {
            using (var context = DaoContextFactory.CreateContext())
            {
                UserStoreFactory.CreateUserDao(context)
                    .AddUser(new UserModel
                    {
                        Name = model.Name,
                        Age = model.Age,
                        MailAddress = model.Mail,
                    });
            }
        }

        public IReadOnlyList<Models.UserDataModel> Find(UserFindModel model)
        {
            using (var context = DaoContextFactory.CreateContext())
            {
                var dao = UserStoreFactory.CreateUserDao(context);

                var users = dao.GetUsers(new UserFindParameter {
                    AgeLower = model.AgeLower,
                    AgeUpper = model.AgeUpper,
                });

                // DatastoreのDTOからBusinessLogicのDTOに変換
                return (from user in users
                        select new Models.UserDataModel
                        {
                            Name = user.Name,
                            Age = user.Age,
                            Mail = user.MailAddress,
                        }).ToList();
            }
        }
        #endregion
    }
}
