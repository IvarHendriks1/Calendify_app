using CalendifyApp.Models;
using CalendifyApp.Utils;

namespace CalendifyApp.Services;

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
        // TODO: Make this method check the password with what is in the database
        if (_context.Admin.Any(x => x.UserName == username))
        {
            Admin admin = _context.Admin.Where(x => x.UserName == username).Single();
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

        if (_context.Users.Any(x => x.First_name == username))
        {
            User user = _context.Users.Where(x => x.First_name == username).Single();
            if (user.Password == EncryptionHelper.EncryptPassword(inputPassword))
            {
                return LoginStatus.Success;
            }
            return LoginStatus.IncorrectPassword;

        }


        return LoginStatus.IncorrectUsername;
    }
}