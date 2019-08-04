using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLib.Datastore.Context;
using TestLib.Datastore.Context.Impl;
using TestLib.Datastore.User.Factory.Impl;
using TestLib.Datastore.User.Daos;
using TestLib.Datastore.User.Models;

namespace TestLibTest.Datastore.User.Daos.Impl
{
    [TestClass]
    public class UserDaoTest
    {
        #region "テスト開始と終了時に呼ばれる処理"
        private static IDaoContext _context;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _context = new DaoContext("Data Source =:memory:;");
            _context.Open();

            var command = _context.GetConnection().CreateCommand();
            command.CommandText = "CREATE TABLE USER(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, age INTEGER, mail TEXT);";
            command.ExecuteNonQuery();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _context.Dispose();
        }
        #endregion


        #region "テスト毎に呼ばれる処理"
        private IUserDao _dao;

        [TestInitialize]
        public void TestInitialize()
        {
            BuildDbData();

            var factory = new UserStoreFactory();
            _dao = factory.CreateUserDao(_context);
        }

        private void BuildDbData()
        {
            _context.BeginTransaction();

            var command = _context.GetConnection().CreateCommand();
            command.CommandText = new StringBuilder()
                .Append("INSERT INTO USER(name, age, mail) VALUES('Yamada', 18, 'yamada@example.com');").AppendLine()
                .Append("INSERT INTO USER(name, age, mail) VALUES('Furukawa', 31, 'furukawa@example.com');").AppendLine()
                .Append("INSERT INTO USER(name, age, mail) VALUES('Kikuchi', 26, 'kikuchi@example.com');").AppendLine()
                .ToString();
            command.ExecuteNonQuery();

            _context.Commit();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var command = _context.GetConnection().CreateCommand();
            command.CommandText = new StringBuilder()
                .Append("DELETE FROM USER;")
                .ToString();
            command.ExecuteNonQuery();
        }
        #endregion



        [TestMethod]
        public void AddUserTest()
        {
            _dao.AddUser(new UserModel
            {
                Name = "Furutate",
                Age = 44,
                MailAddress = "furutate@example.com",
            });


            // 追加されたか確認する
            var command = _context.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM USER;";
            Assert.AreEqual(4L, command.ExecuteScalar());
        }

        [TestMethod]
        public void GetCountTest()
        {
            var count = _dao.GetCount();

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void GetUserTest()
        {
            var command = _context.CreateCommand();
            command.CommandText = "SELECT ID FROM USER WHERE name = 'Yamada';";
            var id = Convert.ToInt64(command.ExecuteScalar());

            var user = _dao.GetUser(id);

            Assert.AreEqual(id, user.ID);
            Assert.AreEqual("Yamada", user.Name);
            Assert.AreEqual(18, user.Age);
            Assert.AreEqual("yamada@example.com", user.MailAddress);
        }

        [TestMethod]
        public void GetUsersTest10To30()
        {
            var users = _dao.GetUsers(new TestLib.Datastore.User.Models.UserFindParameter { AgeLower = 10, AgeUpper = 30});

            Assert.AreEqual(2, users.Count);

            Assert.AreEqual("Yamada", users[0].Name);
            Assert.AreEqual("Kikuchi", users[1].Name);
        }

        [TestMethod]
        public void GetUsersTest20Over()
        {
            var users = _dao.GetUsers(new TestLib.Datastore.User.Models.UserFindParameter { AgeLower = 20 });

            Assert.AreEqual(2, users.Count);

            Assert.AreEqual("Furukawa", users[0].Name);
            Assert.AreEqual("Kikuchi", users[1].Name);
        }

        [TestMethod]
        public void GetUsersTest20Under()
        {
            var users = _dao.GetUsers(new TestLib.Datastore.User.Models.UserFindParameter { AgeUpper = 20 });

            Assert.AreEqual(1, users.Count);

            Assert.AreEqual("Yamada", users[0].Name);
        }

        [TestMethod]
        public void GetUsersTestAll()
        {
            var users = _dao.GetUsers(new TestLib.Datastore.User.Models.UserFindParameter { });

            Assert.AreEqual(3, users.Count);

            Assert.AreEqual("Yamada", users[0].Name);
            Assert.AreEqual("Furukawa", users[1].Name);
            Assert.AreEqual("Kikuchi", users[2].Name);
        }
    }
}
