namespace Client_Server_Refactored.Server
{
    internal interface IMessageOperation
    {
        public bool Handle(Dictionary<string, string> message);
    }
}
