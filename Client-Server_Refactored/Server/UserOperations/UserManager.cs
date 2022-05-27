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
            if (instance == null) instance = new();

            return instance;
        }

        public void AddUser(TcpClient user)
        {
            MessageOperator newOperator = new(user);

            _operators.Add(newOperator);
            newOperator.StartLoop();
        }

        public static void DisconnectUser(MessageOperator user) // For admin to delete user
        {
            user.Disconnect();
            DeleteUser(user);
        }

        public static void DeleteUser(MessageOperator user) // For user to delete himself
        {
            _operators.Remove(user);
        }
    }
}
