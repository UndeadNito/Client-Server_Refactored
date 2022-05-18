using Client_Server_Refactored.Server;

namespace Client_Server_Refactored
{
    public class Program
    {
        private static TCPServer _server = new();

        public static void Main()
        {
            _server.Start();
        }
    
    }
}