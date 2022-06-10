namespace Client_Server_Refactored.Server
{
    internal class RedactEmployee : BasicAction, IUserAction
    {
        private Dictionary<string, string> _answer = new() { { "confirmed", "false"} };

        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;
            return redactEmployee(message);
        }

        private Dictionary<string, string> redactEmployee(Dictionary<string, string> message)
        {
            if (!message.ContainsKey("id"))
            {
                _answer["error"] = "Message have to contain id.";
                return _answer;
            }

            if (_user!.cachedAccountData.privilege <= _dBProvider.GetPrivelegeByID(Convert.ToInt32(message["id"])))
            {
                _answer["error"] = "You can not redact this user.";
                return _answer;
            }

            DBEmployeeRedactor redactor = new(message["id"]);

            if (message.ContainsKey("name")) redactor.Name(message["name"]);
            if (message.ContainsKey("dob")) redactor.DOB(message["dob"]);
            if (message.ContainsKey("gender")) redactor.Gender(message["gender"][0]);
            if (message.ContainsKey("position")) redactor.Position(message["position"]);
            if (message.ContainsKey("position_info")) redactor.PositionInfo(message["positionInfo"]);
            if (message.ContainsKey("other_info")) redactor.OtherInfo(message["other_info"]);

            if (_dBProvider.RedactEmployee(redactor)) _answer["confirmed"] = "true";
            return _answer;
        }
    }
}
