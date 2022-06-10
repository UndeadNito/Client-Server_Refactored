namespace Client_Server_Refactored.Server
{
    internal class GetEmployees : BasicAction, IUserAction
    {
        Dictionary<string, string> _answer = new()
        {
            
        };

        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;
            return getEmployees(message);
        }

        private Dictionary<string, string> getEmployees(Dictionary<string, string> message)
        {
            if (!_user!.loggedIn)
            {
                _answer["error"] = "You have to be logged in to receive any data.";
                return _answer;
            }

            if (message.ContainsKey("filter_criterion") && message.ContainsKey("filter_value"))
            {
                _answer = _dBProvider.GetEmployees(message["filter_criterion"], message["filter_value"]);
                return _answer;
            }

            _answer = _dBProvider.GetEmployees();
            return _answer;
        }
    }
}
