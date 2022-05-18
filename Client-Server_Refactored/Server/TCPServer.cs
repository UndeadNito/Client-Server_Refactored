using System.Net;
using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class TCPServer
    {
        private const int _localPort = 2020;
        private static readonly IPAddress _localAddress = IPAddress.Parse("127.0.0.1");

        private readonly TcpListener _listener;
        private readonly UserManager _manager;

        private static TCPServer? _server;
        private static bool _isActive = false;
        
        public TCPServer()
        { 
            _listener = new TcpListener(_localAddress, _localPort);
            _manager = UserManager.GetManager();
        }

        public void Start()
        {
            _listener.Start();
            _isActive = true;

            new Thread(new ThreadStart(MainLoop)).Start();
        }

        public void Stop() 
        {
            _listener.Stop();
            _isActive = false;
        }

        public bool GetState() { return _isActive; }

        public void TryAddUser()
        {
            if (_listener.Pending()) 
                _manager.AddUser(_listener.AcceptTcpClient());
        }

        private void MainLoop()
        {
            while (_isActive)
            {
                TryAddUser();
            }
        }
    }
}