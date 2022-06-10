namespace Client_Server_Refactored.Server
{
    internal class ActionManager
    {
        private Dictionary<string, IUserAction> actions;

        public ActionManager()
        {
            actions = new();

            actions.Add("nullAction", new NullAction());
            actions.Add("login", new Login());
            actions.Add("register", new Register());
            actions.Add("getEmployees", new GetEmployees());
            actions.Add("getEmployee", new GetEmployee());
            actions.Add("redactEmployee", new RedactEmployee());
            actions.Add("addEmployee", new AddEmployeeAction());
            actions.Add("deleteEmployee", new DeleteEmployeeAction());
        }
        
        public bool AddAction(string name, IUserAction action)
        {
            if (actions.ContainsKey(name)) return false;

            actions.Add(name, action);
            return true;
        }

        public string[] GetActionsNames()
        {
            return actions.Keys.ToArray();
        }

        public IUserAction GetAction(string name)
        {
            if (actions.ContainsKey(name)) return actions[name];

            return actions["nullAction"];
        }
    }
}
