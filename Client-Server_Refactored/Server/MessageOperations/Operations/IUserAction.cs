namespace Client_Server_Refactored.Server
{
    internal interface IUserAction // Register new actions in ActionManager class
    {
        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message);
    }
}
