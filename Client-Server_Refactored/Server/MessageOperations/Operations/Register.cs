using System.Text;

namespace Client_Server_Refactored.Server
{
    internal class Register : AccountingAction, IUserAction
    {
        private Dictionary<string, string> _answer = new()
        {
            { "confirmed", "false" },
            { "error", "" }
        };

        public Register() { }

        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;

            return register(message);
        }

        private Dictionary<string, string> register(Dictionary<string, string> message)
        {
            if (_user!.loggedIn)
            {
                _answer["error"] = "You can't register when logged in.";
                return _answer;
            }

            if (!MessageHasFullData(message, 3))
            {
                _answer["error"] = "Some fields are empty.";
                return _answer;
            }

            if (message["login"].Length > 16)
            {
                _answer["error"] = "Too long login.";
                return _answer;
            }

            if (message["password"].Length > 32)
            {
                _answer["error"] = "Too long password.";
                return _answer;
            }

            string salt = SaltGenerator(8);
            _user!.CacheAccountData( 
                new UserLogInData(
                    message["login"],
                    salt,
                    GeneratePassword(salt, message["password"]),
                    0
                ));
            if (!_dBProvider.AddUser(_user))
            {
                _user!.DeleteCashedData();
                _answer["error"] = "User already exists.";
                return _answer;
            }

            _user!.LogIn();
            _answer["confirmed"] = "true";
            return _answer;
        }

        private static string SaltGenerator(int length)
        {
            char[] symbols = new char[]
            { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f'};

            Random random = new Random();

            StringBuilder builder = new();
            for (int i = 0; i < length; i++)
            {
                builder.Append(symbols[random.Next(16)]);
            }

            return builder.ToString();
        }
    }
}
