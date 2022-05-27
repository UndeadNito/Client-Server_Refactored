namespace Client_Server_Refactored.Server
{
    internal abstract class BasicAction
    {
        public User? _user;
        public DBProvider _dBProvider = DBProvider.GetDBProvider();

        public static bool MessageHasFullData(Dictionary<string, string> data, int count)
        {
            if (!(data.Keys.Count == count)) return false;

            foreach (var key in data.Keys)
            {
                if (data[key].Length == 0) return false;
            }

            return true;
        }
    }
}
