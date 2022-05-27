namespace Client_Server_Refactored.Server
{
    internal class ActionManager
    {
        private Dictionary<string, IUserAction> actions;

        public ActionManager()
        {
            actions = new();

            actions.Add("login", new Login());
            actions.Add("register", new Register());
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
            return actions[name];
        }
    }
}
