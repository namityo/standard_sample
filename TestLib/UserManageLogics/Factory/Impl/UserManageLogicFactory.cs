using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Datastore.Context;
using TestLib.Datastore.Context.Impl;
using TestLib.Datastore.User.Factory;
using TestLib.Datastore.User.Factory.Impl;
using TestLib.UserManageLogics.Logics;
using TestLib.UserManageLogics.Logics.Impl;

namespace TestLib.UserManageLogics.Factory.Impl
{
    public class UserManageLogicFactory : IUserManageLogicFactory
    {
        /// <summary>
        /// DaoContextFactoryを書き換える際はこのメソッドをoverrideする
        /// </summary>
        /// <returns></returns>
        protected virtual IDaoContextFactory CreateDaoContextFactory()
        {
            return new DaoContextFactory();
        }

        /// <summary>
        /// UserStoreFactoryを書き換える際はこのメソッドをoverrideする
        /// </summary>
        /// <returns></returns>
        protected virtual IUserStoreFactory CreateUserStoreFactory()
        {
            return new UserStoreFactory();
        }

        #region "IUserManageLogicFactory 実装"
        public IUserFindLogic CreateUserFindLogic()
        {
            return new UserFindLogic
            {
                DaoContextFactory = CreateDaoContextFactory(),
                UserStoreFactory = CreateUserStoreFactory(),
            };
        }
        #endregion
    }
}
