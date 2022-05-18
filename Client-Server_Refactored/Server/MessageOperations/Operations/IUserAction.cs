namespace Client_Server_Refactored.Server
{
    internal interface IUserAction
    {
        public bool Handle(Dictionary<string, string> message);
    }
}
