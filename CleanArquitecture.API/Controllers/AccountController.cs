using CleanArquitecture.Application.Contracts.Identity;
using CleanArquitecture.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitecture.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        => Ok(await _authService.Login(request));

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Login([FromBody] RegistrationRequest request)
       => Ok(await _authService.Register(request));
}
