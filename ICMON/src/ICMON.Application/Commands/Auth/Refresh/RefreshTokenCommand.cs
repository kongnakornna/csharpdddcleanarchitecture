using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;

namespace ICMON.Application.Commands.Auth.Refresh;

public class RefreshTokenCommand : IRequest<Result<RefreshTokenResponse>>
{
    public string RefreshToken { get; set; } = string.Empty;
}
