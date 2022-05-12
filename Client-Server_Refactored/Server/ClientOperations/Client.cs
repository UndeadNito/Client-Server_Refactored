using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class Client
    {
        public readonly TcpClient client;

        public bool loggedIn { get; private set; }
        private string? _login;
        private string? _salt;
        private string? _password;
        private byte? _privelege;

        public Client(TcpClient client)
        {
            this.client = client;
            loggedIn = false;
        }

        public void LogIn() => loggedIn = true;

        public void LogOut() => loggedIn = false;

        public void CacheLogInData(string login, string salt, string password, byte privelege)
        {
            _login = login;
            _salt = salt;
            _password = password;
            _privelege = privelege;
        }
    }
}
