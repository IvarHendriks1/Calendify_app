using CalendifyApp.Models;
using CalendifyApp.Utils;
using System.Linq;

namespace CalendifyApp.Services
{
    public enum LoginStatus { IncorrectPassword, IncorrectUsernameOrEmail, Success }

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

            return LoginStatus.IncorrectUsernameOrEmail;
        }

        public LoginStatus CheckUserPassword(string email, string inputPassword)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email); // Match email
            if (user != null)
            {
                if (user.Password == inputPassword) // Compare plain text
                {
                    return LoginStatus.Success;
                }
                return LoginStatus.IncorrectPassword;
            }
            return LoginStatus.IncorrectUsernameOrEmail;
        }


    }
}