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

        public void CacheLogInData(ClientLogInData data)
        {
            cachedLodInData = data;
        }
    }

    public struct ClientLogInData
    {
        public readonly int id;
        public readonly string login;
        public readonly string salt;
        public readonly string password;
        public readonly byte privelege;

        public ClientLogInData(int id, string login, string salt, string password, byte privelege)
        {
            this.id = id;
            this.login = login;
            this.salt = salt;
            this.password = password;
            this.privelege = privelege;
        }
    }
}
