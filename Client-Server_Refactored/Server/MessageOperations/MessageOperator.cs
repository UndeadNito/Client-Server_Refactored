using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class MessageOperator
    {
        private readonly TcpClient _user;
        private readonly MessageProvider _provider;

        public bool active { get; private set; }

        public MessageOperator(TcpClient user)
        {
            _user = user;
            _provider = new MessageProvider(user.GetStream());
            active = true;
        }

        public void Disconnect() => active = false;

        public bool StartLoop()
        {
            new Thread(new ThreadStart(MainLoop)).Start();
            return true;
        }

        private void MainLoop()
        {
            while (active)
            {

            }
        }
    }
}
