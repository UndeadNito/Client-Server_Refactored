namespace Client_Server_Refactored.Server
{
    internal class NullAction : IUserAction
    {
        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            return new Dictionary<string, string>() { { "error", "Wrong action" } };
        }
    }
}
