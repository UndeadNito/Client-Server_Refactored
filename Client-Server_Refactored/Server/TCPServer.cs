using System.Net;
using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class TCPServer
    {
        private const int _localPort = 2020;
        private static readonly IPAddress _localAddress = IPAddress.Parse("127.0.0.1");

        private readonly TcpListener _listener;

        private static TCPServer? _server;
        private static bool _isActive = false;
        
        private TCPServer()
        { 
            _listener = new TcpListener(_localAddress, _localPort);
        }

        public static TCPServer GetTCPServer()
        {
            if (_server == null) _server = new TCPServer();

            return _server;
        }

        public void Start()
        {
            _listener.Start();
            _isActive = true;
        }

        public void Stop() 
        {
            _listener.Stop();
            _isActive = false;
        }

        public bool GetState() { return _isActive; }

        public TcpClient? GetClient()
        {
            if (_listener.Pending()) return _listener.AcceptTcpClient();
            return null;
        }
    }
}