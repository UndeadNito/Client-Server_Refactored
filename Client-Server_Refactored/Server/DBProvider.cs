using MySql.Data.MySqlClient;

namespace Client_Server_Refactored.Server
{
    internal class DBProvider : IDisposable
    {
        private const string connectionString = "Database=cssoft;Uid=requester;Pwd=pass;";
        private static readonly MySqlConnection _connection = new MySqlConnection(connectionString);
        private bool _connected = false;

        private DBProvider() { }

        public DBProvider GetDBProvider()
        {
            if (!_connected) { _connection.Open(); }

            return this;
        }

        private static MySqlDataReader ExecuteCommand(string command)
        {
            MySqlCommand SQLcommand = new MySqlCommand(command, _connection);

            MySqlDataReader reader = SQLcommand.ExecuteReader();
            return reader;
        }

        public static ClientLogInData? RequestDataByLogin(string login)
        {
            MySqlDataReader reader = ExecuteCommand($"select * from user where login = \'{login}\'");
            reader.Read();
            if (!reader.HasRows) { reader.Close(); return null; }

            var userData = new ClientLogInData(
                reader.GetInt32(0), 
                reader.GetString(1), 
                reader.GetString(2), 
                reader.GetString(3), 
                reader.GetByte(4));

            reader.Close();
            return userData;
        }

        public static void AddUser(string login, string salt, string password, int privelegeGroup)
        {
            MySqlDataReader reader = ExecuteCommand("INSERT INTO user (`login`, `salt`, `password`, `securityGroup`) " +
                                                    $"VALUES (\'{login}\', \'{salt}\', \'{password}\', \'{privelegeGroup}\')");
            reader.Close();
        }

        public static void DeleteUser(string login)
        {
            MySqlDataReader reader = ExecuteCommand($"delete from user where login = \'{login}\'");
            reader.Close();
        }


        public void Dispose()
        {
            _connected = false;
            _connection.Close();
        }
    }
}
