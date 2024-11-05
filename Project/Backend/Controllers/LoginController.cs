using System.Text;
using Microsoft.AspNetCore.Mvc;
using CalendifyApp.Services;
using CalendifyApp.Models;

namespace CalendifyApp.Controllers;


[Route("api")]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService, MyContext context)
    {
        _loginService = loginService;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginBody loginBody)
    {
        if (_loginService.CheckPassword(loginBody.Username, loginBody.Password) == LoginStatus.Success){
            HttpContext.Session.SetString("AdminLoggedIn", $"{loginBody.Username}");
            return Ok("Succesfully logged in.");
            
        }

        return Unauthorized($"{_loginService.CheckPassword(loginBody.Username, loginBody.Password)}");
    }

    [HttpGet("IsAdminLoggedIn")]
    public IActionResult IsAdminLoggedIn()
    {
        // TODO: This method should return a status 200 OK when logged in, else 403, unauthorized
        if (HttpContext.Session.GetString("AdminLoggedIn") is null){
            return Unauthorized("You are not logged in");
        }
        return Ok($"{HttpContext.Session.GetString("AdminLoggedIn")}");
        
    }

    [HttpGet("Logout")]
    public IActionResult Logout()
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