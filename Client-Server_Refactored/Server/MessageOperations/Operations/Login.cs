namespace Client_Server_Refactored.Server
{
    internal class Login : IUserAction
    {
        private User _user;

        public Login(User user)
        {
            _user = user;
        }

        private void login()
        {

        }

        public bool Handle(Dictionary<string, string> message)
        {
            throw new NotImplementedException(); //TODO end this when complete database stage
        }
    }
}
