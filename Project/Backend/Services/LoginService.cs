using CalendifyApp.Models;
using CalendifyApp.Utils;
using System.Linq;

namespace CalendifyApp.Services
{
    public enum LoginStatus { IncorrectPassword, IncorrectUsername, Success }

    public enum ADMIN_SESSION_KEY { adminLoggedIn }

    public class LoginService : ILoginService
    {
        private readonly MyContext _context;

        public LoginService(MyContext context)
        {
            _context = context;
        }

        public LoginStatus CheckPassword(string username, string inputPassword)
        {
            // Check if the admin exists in the database
            var admin = _context.Admin.SingleOrDefault(x => x.Username == username);
            if (admin != null)
            {
                // Validate the password
                if (admin.Password == EncryptionHelper.EncryptPassword(inputPassword))
                {
                    return LoginStatus.Success;
                }
                return LoginStatus.IncorrectPassword;
            }

            return LoginStatus.IncorrectUsername;
        }

        public LoginStatus CheckUserPassword(string username, string inputPassword)
        {
            // Check if the user exists in the database
            var user = _context.Users.SingleOrDefault(x => x.FirstName == username);
            if (user != null)
            {
                // Validate the password
                if (user.Password == EncryptionHelper.EncryptPassword(inputPassword))
                {
                    return LoginStatus.Success;
                }
                return LoginStatus.IncorrectPassword;
            }

            return LoginStatus.IncorrectUsername;
        }
    }
}
