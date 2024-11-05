namespace CalendifyApp.Services;

public interface ILoginService
{
    public LoginStatus CheckPassword(string username, string inputPassword);

    public LoginStatus CheckUserPassword(string username, string inputPassword);
}