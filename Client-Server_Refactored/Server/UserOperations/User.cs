namespace Client_Server_Refactored.Server
{
    internal class User
    {
        public UserLogInData cachedAccountData;

        public bool loggedIn { get; private set; }

        public User()
        {
            loggedIn = false;
        }

        public void LogIn() => loggedIn = true;

        public void LogOut() => loggedIn = false;

        public void CacheAccountData(UserLogInData data)
        {
            cachedAccountData = data;
        }
        public void DeleteCashedData()
        {
            cachedAccountData = new UserLogInData();
        }
    }

    public struct UserLogInData
    {
        public readonly string login;
        public readonly string salt;
        public readonly string password;
        public readonly byte privilege;

        public UserLogInData(string login, string salt, string password, byte privilege)
        {
            this.login = login;
            this.salt = salt;
            this.password = password;
            this.privilege = privilege;
        }
    }
}
