using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ICMON.Application.Commands.Auth.Login;
using ICMON.Application.Commands.Auth.Register;
using ICMON.Application.Commands.Auth.Refresh;
using ICMON.Application.Commands.Auth.Logout;
using ICMON.Application.Queries.Auth.GetCurrentUser;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;

namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return Unauthorized(result);
        return Ok(result);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<RegisterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<RegisterResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(Result<RefreshTokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<RefreshTokenResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return Unauthorized(result);
        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _mediator.Send(new GetCurrentUserQuery());
        if (!result.IsSuccess)
            return Unauthorized(result);
        return Ok(result);
    }
}
