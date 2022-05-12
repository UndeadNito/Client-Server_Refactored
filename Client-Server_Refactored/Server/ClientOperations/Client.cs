using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal struct Client
    {
        public readonly TcpClient client { get; }

        public Client(TcpClient client)
        {
            this.client = client;
        }


    }
}
