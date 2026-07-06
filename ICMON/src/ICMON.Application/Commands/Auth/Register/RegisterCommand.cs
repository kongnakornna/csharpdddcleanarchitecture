using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;

namespace ICMON.Application.Commands.Auth.Register;

public class RegisterCommand : IRequest<Result<RegisterResponse>>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
