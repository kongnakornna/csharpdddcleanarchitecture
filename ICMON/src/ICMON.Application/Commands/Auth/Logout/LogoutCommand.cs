using MediatR;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Auth.Logout;

public class LogoutCommand : IRequest<Result<bool>>
{
    public string RefreshToken { get; set; } = string.Empty;
}
