namespace Client_Server_Refactored.Server
{
    internal class User
    {
        public UserLogInData cachedLodInData;

        public bool loggedIn { get; private set; }

        public User()
        {
            loggedIn = false;
        }

        public void LogIn() => loggedIn = true;

        public void LogOut() => loggedIn = false;

        public void CacheLogInData(UserLogInData data)
        {
            cachedLodInData = data;
        }
    }

    public struct UserLogInData
    {
        public readonly int id;
        public readonly string login;
        public readonly string salt;
        public readonly string password;
        public readonly byte privelege;

        public UserLogInData(int id, string login, string salt, string password, byte privelege)
        {
            this.id = id;
            this.login = login;
            this.salt = salt;
            this.password = password;
            this.privelege = privelege;
        }
    }
}
