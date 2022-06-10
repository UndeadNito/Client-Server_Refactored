using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server_Refactored.Server
{
    internal class GetEmployee : BasicAction, IUserAction
    {
        Dictionary<string, string> _answer = new()
        {
            
        };

        public Dictionary<string, string> Handle(User user, Dictionary<string, string> message)
        {
            _user = user;
            return getEmployee(message);
        }

        private Dictionary<string, string> getEmployee(Dictionary<string, string> message)
        {
            if (!_user!.loggedIn)
            {
                _answer["error"] = "You have to be logged in to receive any data.";
                return _answer;
            }

            if (!message.ContainsKey("id"))
            {
                _answer["error"] = "Message does not contain user id.";
                return _answer;
            }

            _answer = (_dBProvider.GetEmployee(Convert.ToInt32(message["id"])));

            if (_user!.cachedAccountData.privilege > _dBProvider.GetPrivelegeByID(Convert.ToInt32(message["id"])))
                _answer["can_redact"] = "true";

            return _answer;
        }
    }
}
