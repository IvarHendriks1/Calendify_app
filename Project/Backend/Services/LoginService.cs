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

        public int Register(User user)
        {
            if (_context.Users.SingleOrDefault(x => x.Email == user.Email) != null) return 3;
            if (user.Password.Length < 6) return 2;
            if (!user.Email.Contains("@") || !user.Email.Contains(".")) return 1;
            user.Password = EncryptionHelper.EncryptPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return 0;
        }

        // public int ForgotPassword(string email){
        //     if (email is null) return 0;
        //     if (_context.Users.SingleOrDefault(x => x.Email == email) == null) return 0;
        //     //random 6 digit number


        // }
    }
}
