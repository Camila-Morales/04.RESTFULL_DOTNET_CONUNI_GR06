using System.Security.Cryptography;
using System.Text;

namespace WS_Con_Uni_RESTFULL.ec.edu.monster.modelo
{
    public class LoginModelo
    {
        private String username;
        private String password;
        private static readonly Dictionary<string, string> users = new Dictionary<string, string>();

        static LoginModelo()
        {
            users["MONSTER"] = HashPassword("MONSTER9");
        }

        public bool login(string username, string password)
        {
            if (users.TryGetValue(username, out string storedPassword))
            {
                return storedPassword == HashPassword(password);
            }
            return false;
        }

        public bool register(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                return false;
            }
            users[username] = HashPassword(password);
            return true;
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

    }
}
