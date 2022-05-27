namespace Client_Server_Refactored.Server
{
    internal class GetData : BasicAction, IUserAction
    {
        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;
            return getData(message);
        }

        private Dictionary<string, string> getData(Dictionary<string, string> message)
        {
            return null;
        }
    }
}
