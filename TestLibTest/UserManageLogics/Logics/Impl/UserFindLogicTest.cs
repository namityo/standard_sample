using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLib.Datastore.Context;
using TestLib.Datastore.User.Daos;
using TestLib.Datastore.User.Factory;
using TestLib.UserManageLogics.Factory.Impl;

using Moq;

namespace TestLibTest.UserManageLogics.Logics.Impl
{
    [TestClass]
    public class UserFindLogicTest
    {
        [TestMethod]
        public void FindTestAll()
        {
            var factory = new TestFactory();
            var logic = factory.CreateUserFindLogic();

            var users = logic.Find(new TestLib.UserManageLogics.Models.UserFindModel { });

            Assert.AreEqual(3, users.Count);
        }

        #region "テスト用のMockを用意"
        class TestFactory : UserManageLogicFactory
        {
            protected override IDaoContextFactory CreateDaoContextFactory()
            {
                // ContextをMock化
                var mockContext = new Mock<IDaoContext>();

                // FactoryをMock化
                var mockFactory = new Mock<IDaoContextFactory>();
                mockFactory.Setup(c => c.CreateContext()).Returns(mockContext.Object);

                return mockFactory.Object;
            }

            protected override IUserStoreFactory CreateUserStoreFactory()
            {
                // DaoをMock化
                var mockDao = new Mock<IUserDao>();
                mockDao.Setup(c => c.GetUsers(It.IsAny<TestLib.Datastore.User.Models.UserFindParameter>()))
                    .Returns(new List<TestLib.Datastore.User.Models.UserModel> {
                        new TestLib.Datastore.User.Models.UserModel { ID = 1, Name = "test", Age = 20, MailAddress = "test@test.com"},
                        new TestLib.Datastore.User.Models.UserModel { ID = 2, Name = "test", Age = 20, MailAddress = "test@test.com"},
                        new TestLib.Datastore.User.Models.UserModel { ID = 3, Name = "test", Age = 20, MailAddress = "test@test.com"},
                    });

                // FactoryをMock化
                var mockFactory = new Mock<IUserStoreFactory>();
                mockFactory.Setup(c => c.CreateUserDao(It.IsAny<IDaoContext>()))
                    .Returns(mockDao.Object);

                return mockFactory.Object;
            }
        }
        #endregion
    }
}
