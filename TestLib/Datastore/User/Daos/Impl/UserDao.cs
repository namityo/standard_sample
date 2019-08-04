using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Datastore.Context;
using TestLib.Datastore.User.Models;

namespace TestLib.Datastore.User.Daos.Impl
{
    class UserDao : DaoBase, IUserDao
    {
        public UserDao(IDaoContext context) : base(context)
        {
        }

        public void AddUser(UserModel user)
        {
            var command = DaoContext.CreateCommand();
            command.CommandText = "INSERT INTO USER(name, age, mail) VALUES(:name, :age, :mail);";

            var paramName = command.CreateParameter();
            paramName.ParameterName = "name";
            paramName.Value = user.Name;
            command.Parameters.Add(paramName);

            var paramAge = command.CreateParameter();
            paramAge.ParameterName = "age";
            paramAge.Value = user.Age;
            command.Parameters.Add(paramAge);

            var paramMail = command.CreateParameter();
            paramMail.ParameterName = "mail";
            paramMail.Value = user.MailAddress;
            command.Parameters.Add(paramMail);

            command.ExecuteNonQuery();
        }

        public long GetCount()
        {
            var command = DaoContext.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM USER;";
            if (command.ExecuteScalar() is long result)
            {
                return result;
            }
            else
            {
                // SQLのミスで取得した型が違う場合
                // TODO 独自のExceptionを作ってthrowすると良い。
                throw new Exception("Error");
            }
        }

        public UserModel GetUser(long id)
        {
            var command = DaoContext.CreateCommand();
            command.CommandText = "SELECT * FROM USER WHERE ID = :id";

            // TODO ここらへんは拡張メソッドとかを作ると綺麗になる
            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;
            command.Parameters.Add(param);

            var reader = command.ExecuteReader();

            // TODO DbDataReaderからModelを生成するBuilderクラスを作ると綺麗になる。
            if (reader.HasRows && reader.Read())
            {
                // 先頭1件のみ取得する
                return new UserModel
                {
                    ID = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Age = reader.GetInt32(2),
                    MailAddress = reader.GetString(3),
                };
            }
            else
            {
                // 見つからない場合
                // TODO 独自のExceptionを作ってthrowすると良い。
                throw new Exception("Not found.");
            }
        }

        public IReadOnlyList<UserModel> GetUsers(UserFindParameter parameter)
        {
            var command = DaoContext.CreateCommand();
            var builder = new StringBuilder();
            builder.Append("SELECT * FROM USER WHERE 1=1 ");

            // TODO ここらへんは拡張メソッドとかを作ると綺麗になる
            if (parameter.AgeLower.HasValue)
            {
                builder.Append("AND age >= :age_lower ");
                var p = command.CreateParameter();
                p.ParameterName = "age_lower";
                p.Value = parameter.AgeLower.Value;
                command.Parameters.Add(p);
            }
            if (parameter.AgeUpper.HasValue)
            {
                builder.Append("AND age <= :age_upper ");
                var p = command.CreateParameter();
                p.ParameterName = "age_upper";
                p.Value = parameter.AgeUpper.Value;
                command.Parameters.Add(p);
            }

            command.CommandText = builder.Append(";").ToString();
            var reader = command.ExecuteReader();

            // TODO DbDataReaderからModelを生成するBuilderクラスを作ると綺麗になる。
            var result = new List<UserModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new UserModel
                    {
                        ID = reader.GetInt64(0),
                        Name = reader.GetString(1),
                        Age = reader.GetInt32(2),
                        MailAddress = reader.GetString(3),
                    });
                }
            }
            return result;
        }
    }
}
