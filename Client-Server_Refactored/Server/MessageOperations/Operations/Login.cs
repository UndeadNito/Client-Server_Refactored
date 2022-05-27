namespace Client_Server_Refactored.Server
{
    internal class Login : AccountingAction, IUserAction
    {

        private Dictionary<string, string> _answer = new()
        {
            { "confirmed", "false" },
            { "error", "" }
        };

        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;
            return login(message);
        }


        private Dictionary<string, string> login(Dictionary<string, string> message)
        {
                    //Full data check

            if (!MessageHasFullData(message, 3))
            {
                _answer["error"] = "Some fields are empty.";
                return _answer;
            }

                    //Login & password length check

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

                    //Existance check

            var accountData = _dBProvider.RequestDataByLogin(message["login"]);
            if (accountData == null)
            {
                _answer["error"] = "Wrong login or password";
                return _answer;
            }

                    //Correct account data check

            _user!.CacheAccountData((UserLogInData)accountData);
            if (!ComparePassword(
                    _user.cachedAccountData.password,
                    _user.cachedAccountData.salt,
                    message["password"]))
            {
                _answer["error"] = "Wrong login or password";
                return _answer;
            }

                    //Correct data answer forming

            _user.LogIn();
            _answer["confirmed"] = "true";

            return _answer;
        }
    }
}
