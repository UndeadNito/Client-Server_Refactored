using MySql.Data.MySqlClient;

namespace Client_Server_Refactored.Server
{
    internal class DBProvider
    {
        private const string connectionString = "Database=cssoftr;Uid=requester;Pwd=pass;host=localhost;";
        private readonly MySqlConnection _connection = new(connectionString);

        private static DBProvider? _instance;

        private DBProvider() { }

        public static DBProvider GetDBProvider()
        {
            if (_instance == null) 
            {
                _instance = new();
                _instance._connection.Open();
            }

            return _instance;
        }

        public UserLogInData? RequestDataByLogin(string login)
        {
            MySqlDataReader reader = GetData($"select * from user where login = \'{login}\'");
            reader.Read();
            if (!reader.HasRows) { reader.Close(); return null; }

            UserLogInData userData = new(
                reader.GetString(1), 
                reader.GetString(2), 
                reader.GetString(3), 
                reader.GetByte(4)
                );

            reader.Close();
            return userData;
        }

        public bool AddUser(string login, string salt, string password, int privilege)
        {
            if (RequestDataByLogin(login) != null) return false;

            SendData(
                @$"INSERT INTO user 
                       (`login`, `salt`, `password`, `privilege`)
                 VALUES
                       ('{login}', '{salt}', '{password}', '{privilege}')"
                );

            return true;
        }

        public bool AddUser(User user)
        {
            return AddUser(
                user.cachedAccountData.login,
                user.cachedAccountData.salt,
                user.cachedAccountData.password,
                user.cachedAccountData.privilege
                );
        }

        public void DeleteUser(string login)
        {
            SendData($"delete from user where login = \'{login}\'");
        }


        private MySqlDataReader GetData(string command)
        {
            MySqlCommand SQLcommand = new(command, _connection);

            MySqlDataReader reader = SQLcommand.ExecuteReader();
            return reader;
        }

        private void SendData(string command)
        {
            MySqlCommand SQLcommand = new(command, _connection);
            SQLcommand.ExecuteNonQuery();
        }
    }
}
