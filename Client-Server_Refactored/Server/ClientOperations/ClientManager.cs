using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace Client_Server_Refactored.Server
{
    internal class ClientManager
    {
        private static List<Thread> _clients = new List<Thread>();

        private ClientManager() { }

        private static void AddClient(TcpClient client)
        {
            
        }
    }
}
