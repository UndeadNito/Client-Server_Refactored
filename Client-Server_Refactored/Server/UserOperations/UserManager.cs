using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class UserManager
    {
        private static UserManager? instance;

        private static List<MessageOperator> _operators = new();

        private UserManager() { }

        public static UserManager GetManager()
        {
            if (instance == null) instance = new UserManager();

            return instance;
        }

        public void AddUser(TcpClient user)
        {
            MessageOperator newOperator = new(user);

            _operators.Add(newOperator);
            newOperator.StartLoop();
        }
    }
}
