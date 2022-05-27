using System.Security.Cryptography;
using System.Text;

namespace Client_Server_Refactored.Server
{
    internal abstract class AccountingAction : BasicAction
    {
        private const string PEPPER = "f8c1add3";

        public static bool ComparePassword(string passwordHash, string salt, string givenPassword)
        {
            // pepper + salt + password = password hash

            string calculatedPassword = GeneratePassword(salt, givenPassword);
            if (passwordHash == calculatedPassword) return true;

            return false;
        }

        public static string GeneratePassword(string salt, string givenPassword)
        {
            return ComputeHash(PEPPER + salt + givenPassword);
        }

        public static string ComputeHash(string input)
        {
            if (input == "") { return ""; }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hashBytes = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(input));

                StringBuilder builder = new();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
