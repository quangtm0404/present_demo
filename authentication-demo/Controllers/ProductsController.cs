using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authentication_demo.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetNotAuthor()
    {
        return Ok("Endpoint not has authorization"); 
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("admins")] 
    public IActionResult GetAuthorizeAdmin()
    {
        return Ok("Admin Only");
    }

    [Authorize]
    [HttpGet("users")]
    public IActionResult GetAuthorize()
    {
        return Ok("All Authenticated User Endpoints");
    }
    
   
    [Authorize(Policy = "StudentOnly")]
    [HttpGet("students")]
    public IActionResult GetForStudent()
    {
        return Ok("Endpoint for Student only");
    }

    [Authorize(Policy = "AtLeast21")] 
    [HttpGet("adults")]
    public IActionResult GetAdultsOnly() 
    {
        return Ok("Endpoints for who 21 years old or older only!");
    }
}
