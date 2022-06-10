using MySql.Data.MySqlClient;
using System.Text;

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
                reader.GetInt32(0),
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

        public int GetPrivelegeByID(int id)
        {
            var reader = GetData($@"
                                    SELECT privilege
                                    FROM user
                                    WHERE id = {id}
                                    ");

            reader.Read();

            var value = reader.GetByte(0);
            reader.Close();
            return value;
        }

        public Dictionary<string, string> GetEmployee(int id)
        {
            var reader = GetData($@"
                                    SELECT *
                                    FROM employee
                                    WHERE user_id = {id}
                                ");

            Dictionary<string, string> answer = new();

            reader.Read();
            answer.Add("id", reader.GetString(0));
            answer.Add("name", reader.GetString(1));
            answer.Add("dob", reader.GetDateTime(2).ToString("yyyy-MM-dd"));
            answer.Add("gender", reader.GetString(3));
            answer.Add("position", reader.GetString(4));
            answer.Add("position_info", reader.GetString(5));
            answer.Add("other_info", reader.GetString(6));
            reader.Close();

            return answer;
        }

        public Dictionary<string, string> GetEmployees()
        {
            var reader = GetData($@"
                                    SELECT user_id, name, position
                                    FROM employee
                                ");

            Dictionary<string, string> answer = new();

            while (reader.Read())
            {
                answer.Add(reader.GetString(0), $"{reader.GetString(1)}  {reader.GetString(2)}");
            }
            reader.Close();

            return answer;
        }

        public Dictionary<string, string> GetEmployees(string criterion, string value)
        {
            var reader = GetData($@"
                                    SELECT user_id, name, position
                                    FROM employee
                                    WHERE {criterion} LIKE '{value}%'
                                ");

            Dictionary<string, string> answer = new();

            while (reader.Read())
            {
                answer.Add(reader.GetString(0), $"{reader.GetString(1)}  {reader.GetString(2)}");
            }
            reader.Close();

            return answer;
        }

        public bool RedactEmployee(DBEmployeeRedactor redactor)
        {
            return SendData(redactor.ToString());
        }

        public bool AddEmployee(
            string id,
            string name,
            string dob,
            string gender,
            string position,
            string positionInfo,
            string otherInfo)
        {
            return SendData($@"INSERT INTO employee
                               VALUES ({id}, '{name}', '{dob}', '{gender}', '{position}', '{positionInfo}', '{otherInfo}')
                            ");
        }

        public bool DeleteEmployee(string id)
        {
            return SendData($"DELETE FROM employee WHERE user_id = {id}");
        }


        private MySqlDataReader GetData(string command)
        {
            MySqlDataReader reader;
            MySqlCommand SQLcommand = new(command, _connection);

            try
            {
                reader = SQLcommand.ExecuteReader();
            }
            catch (Exception)
            {
                return new MySqlCommand("select * from user limit 0", _connection).ExecuteReader();
            }

            return reader;
        }

        private bool SendData(string command)
        {
            MySqlCommand SQLcommand = new(command, _connection);
            try
            {
                SQLcommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }

    internal class DBEmployeeRedactor
    {
        private static string _header = $"UPDATE employee SET ";
        private readonly string _footer;

        private readonly StringBuilder _builder;

        public DBEmployeeRedactor(string id)
        {
            _footer = $"WHERE user_id = {id}";

            _builder = new(_header);
        }

        public DBEmployeeRedactor Name(string name)
        {
            _builder.Append($"name = '{name}', ");
            return this;
        }

        public DBEmployeeRedactor DOB(string dob)
        {
            _builder.Append($"dob = '{dob}', ");
            return this;
        }

        public DBEmployeeRedactor Gender(char gender)
        {
            _builder.Append($"gender = '{gender}', ");
            return this;
        }

        public DBEmployeeRedactor Position(string position)
        {
            _builder.Append($"position = '{position}', ");
            return this;
        }

        public DBEmployeeRedactor PositionInfo(string positionInfo)
        {
            _builder.Append($"position_info = '{positionInfo}', ");
            return this;
        }

        public DBEmployeeRedactor OtherInfo(string otherInfo)
        {
            _builder.Append($"other_info = '{otherInfo}', ");
            return this;
        }

        public override string ToString()
        {
            _builder.Remove(_builder.Length - 2, 1);
            _builder.Append(_footer);
            return _builder.ToString();
        }
    }
}
