namespace Client_Server_Refactored.Server
{
    internal class DeleteEmployeeAction : BasicAction, IUserAction
    {
        private Dictionary<string, string> _answer = new()
        {
            { "confirmed", "false"}
        };

        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;
            return deleteEmployeeAction(message);
        }

        private Dictionary<string, string> deleteEmployeeAction(Dictionary<string, string> message)
        {
            if (!message.ContainsKey("id"))
            {
                _answer["error"] = "Message hove to contain id.";
                return _answer;
            }

            if (_user!.cachedAccountData.privilege <= _dBProvider.GetPrivelegeByID(Convert.ToInt32(message["id"])))
            {
                _answer["error"] = "You can not delete this employee.";
                return _answer;
            }

            if (!_dBProvider.DeleteEmployee(message["id"]))
            {
                _answer["error"] = "Unknown error.";
                return _answer;
            }

            _answer["confirmed"] = "true";
            return _answer;
        }
    }
}
