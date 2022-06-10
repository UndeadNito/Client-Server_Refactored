namespace Client_Server_Refactored.Server
{
    internal class AddEmployeeAction : BasicAction, IUserAction
    {
        private Dictionary<string, string> _answer = new()
        {
            { "confirmed", "false"}
        };

        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;
            return addEmployeeAction(message);
        }

        private Dictionary<string, string> addEmployeeAction(Dictionary<string, string> message)
        {
            if (!message.ContainsKey("login") &&
                message.ContainsKey("name") &&
                message.ContainsKey("dob") &&
                message.ContainsKey("gender") &&
                message.ContainsKey("position") &&
                message.ContainsKey("position_info") &&
                message.ContainsKey("other_info"))
            {
                _answer["error"] = "Fields can not be empty.";
                return _answer;
            }

            UserLogInData? userToImployeeAccount = _dBProvider.RequestDataByLogin(message["login"]);

            if (userToImployeeAccount == null)
            {
                _answer["error"] = "User with this login does not exists.";
                return _answer;
            }

            if (_user!.cachedAccountData.privilege <= userToImployeeAccount.Value.privilege)
            {
                _answer["error"] = "You can not redact this user.";
                return _answer;
            }

            if (!_dBProvider.AddEmployee(
                userToImployeeAccount.Value.id.ToString(),
                message["name"],
                message["dob"],
                message["gender"],
                message["position"],
                message["position_info"],
                message["other_info"]))
            {
                _answer["error"] = "Your data contain some error.";
                return _answer;
            }

            _answer["confirmed"] = "true";
            return _answer;
        }
    }
}
