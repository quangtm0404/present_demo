using System.Net;
using authentication_demo.Services.Interfaces;
using authentication_demo.ViewModels;
using authentication_demo.ViewModels.CreateModels;
using Microsoft.AspNetCore.Mvc;

namespace authentication_demo.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserModel model)
    {
        var result = await _authService.RegisterAsync(model);
        if(result) 
        {
            return StatusCode(StatusCodes.Status201Created, "Created successfully!");
        } else {
            return StatusCode(StatusCodes.Status400BadRequest, "Create failed!");
        }
    }

    // [HttpGet]
    // public async Task<IActionResult> Login([FromBody] LoginRequestModel model) 
    // {

    // }

    [HttpGet]
    public async Task<IActionResult> AssignRole([FromQuery] string email, [FromQuery] string role) 
    {
        var result = await _authService.AssignRoleAsync(email, role);
         if(result) 
        {
            return StatusCode(StatusCodes.Status201Created, "Assign Role successfully!");
        } else {
            return StatusCode(StatusCodes.Status400BadRequest, "Create failed!");
        }
    }

    [HttpPost("users")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel model) 
    {
        var result = await _authService.LoginAsync(model);
        return Ok(result);
    }
}