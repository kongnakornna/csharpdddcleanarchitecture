using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;

namespace ICMON.Application.Commands.Auth.Login;

public class LoginCommand : IRequest<Result<LoginResponse>>
{
    public string UsernameOrEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
