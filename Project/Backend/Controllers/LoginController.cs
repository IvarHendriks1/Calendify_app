using System.Text;
using Microsoft.AspNetCore.Mvc;
using CalendifyApp.Services;
using CalendifyApp.Models;
using CalendifyApp.Filters;

namespace CalendifyApp.Controllers;


[Route("api")]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet("SimulateAdminLogin")]
    public IActionResult SimulateAdminLogin()
    {
        HttpContext.Session.SetString("AdminLoggedIn", "admin1");
        return Ok("Simulated admin login successful.");
    }

    [HttpGet("SimulateUserLogin")]
    public IActionResult SimulateUserLogin()
    {
        HttpContext.Session.SetString("UserLoggedIn", "John");
        return Ok("Simulated user login successful.");
    }


    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginBody loginBody)
    {
        if (loginBody.Password == null || loginBody.Username == null) return BadRequest("Invallid input");
        if (_loginService.CheckPassword(loginBody.Username, loginBody.Password) == LoginStatus.Success)
        {
            HttpContext.Session.SetString("AdminLoggedIn", $"{loginBody.Username}");
            return Ok("Succesfully logged in.");

        }
        else if (_loginService.CheckUserPassword(loginBody.Username, loginBody.Password) == LoginStatus.Success)
        {
            HttpContext.Session.SetString("UserLoggedIn", $"{loginBody.Username}");
            return Ok("Succesfully logged in.");
        }

        return Unauthorized($"{_loginService.CheckPassword(loginBody.Username, loginBody.Password)}");
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] User user)
    {
        if (user is null) return BadRequest("Invallid input");
        switch (_loginService.Register(user))
        {
            case 3:
                return BadRequest("Email already in use by another account, try 'forgot password'.");
            case 2:
                return BadRequest("Password must be at least 6 characters long.");
            case 1:
                return BadRequest("Please use a valid Email.");
            case 0:
                return Ok($"Succesfully Registerd {user}");
            default:
                return BadRequest("Something went wrong");
        }

    }

    [HttpGet("IsAdminLoggedIn")]
    public IActionResult IsAdminLoggedIn()
    {
        // TODO: This method should return a status 200 OK when logged in, else 403, unauthorized
        if (HttpContext.Session.GetString("AdminLoggedIn") is null)
        {
            return Unauthorized("You are not logged in");
        }
        return Ok($"{HttpContext.Session.GetString("AdminLoggedIn")}");

    }

    [HttpGet("IsUserLoggedIn")]
    public IActionResult IsUserLoggedIn()
    {
        // TODO: This method should return a status 200 OK when logged in, else 403, unauthorized
        if (HttpContext.Session.GetString("UserLoggedIn") is null)
        {
            return Unauthorized("You are not logged in");
        }
        return Ok($"{HttpContext.Session.GetString("UserLoggedIn")}");

    }
    [AuthorizationFilter]
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserLoggedIn");
        return Ok("Logged out");
    }
    [AdminFilter]
    [HttpGet("AdminLogout")]
    public IActionResult AdminLogout()
    {

        HttpContext.Session.Remove("AdminLoggedIn");
        return Ok("Logged out");
    }

}



public class LoginBody
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}