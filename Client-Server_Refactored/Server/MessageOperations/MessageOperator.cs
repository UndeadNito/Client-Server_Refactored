using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class MessageOperator
    {
        private readonly TcpClient _userClient;
        private readonly User _user;
        private readonly MessageProvider _provider;
        private readonly ActionManager _actionManager;

        public bool active { get; private set; }

        public MessageOperator(TcpClient user)
        {
            _userClient = user;
            _user = new();
            _provider = new(user.GetStream());
            _actionManager = new();

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
            while (_userClient.Connected && active)
            {
                if (!_provider.NewMessage()) 
                {
                    Thread.Sleep(10);
                    continue;
                }

                var message = _provider.GetMessage();

                IUserAction action = _actionManager.GetAction(message["action"]);
                var answer = action.Handle(_user, message);
                _provider.SendMessage(answer);
            }

            UserManager.DeleteUser(this);
        }
    }
}
