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
        return LoginStatus.IncorrectPassword;
    }
}