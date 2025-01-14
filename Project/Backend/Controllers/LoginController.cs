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
        if (loginBody.Password == null || (loginBody.Username == null && loginBody.Email == null))
            return BadRequest("Invalid input");

        // Admin login
        if (!string.IsNullOrEmpty(loginBody.Username) && 
            _loginService.CheckPassword(loginBody.Username, loginBody.Password) == LoginStatus.Success)
        {
            HttpContext.Session.SetString("AdminLoggedIn", $"{loginBody.Username}");
            return Ok("Successfully logged in as admin.");
        }

        // User login
        if (!string.IsNullOrEmpty(loginBody.Email) && 
            _loginService.CheckUserPassword(loginBody.Email, loginBody.Password) == LoginStatus.Success)
        {
            HttpContext.Session.SetString("UserLoggedIn", $"{loginBody.Email}");
            return Ok("Successfully logged in as user.");
        }

        return Unauthorized("Invalid email, username, or password.");
    }

    [HttpGet("IsAdminLoggedIn")]
    public IActionResult IsAdminLoggedIn()
    {
        if (HttpContext.Session.GetString("AdminLoggedIn") is null)
        {
            return Unauthorized("You are not logged in as admin.");
        }
        return Ok($"{HttpContext.Session.GetString("AdminLoggedIn")}");
    }

    [HttpGet("IsUserLoggedIn")]
    public IActionResult IsUserLoggedIn()
    {
        if (HttpContext.Session.GetString("UserLoggedIn") is null)
        {
            return Unauthorized("You are not logged in as user.");
        }
        return Ok($"{HttpContext.Session.GetString("UserLoggedIn")}");
    }

    [AuthorizationFilter]
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserLoggedIn");
        return Ok("Logged out.");
    }

    [AdminFilter]
    [HttpGet("AdminLogout")]
    public IActionResult AdminLogout()
    {
        HttpContext.Session.Remove("AdminLoggedIn");
        return Ok("Logged out.");
    }
}

public class LoginBody
{
    public string? Username { get; set; }
    public string? Email { get; set; } // Added email for user login
    public string? Password { get; set; }
}
