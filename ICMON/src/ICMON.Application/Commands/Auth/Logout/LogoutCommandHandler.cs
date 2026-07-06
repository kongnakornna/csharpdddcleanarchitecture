using MediatR;
using ICMON.Application.Common;
using ICMON.Application.Services;

namespace ICMON.Application.Commands.Auth.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<bool>>
{
    private readonly ITokenService _tokenService;

    public LogoutCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _tokenService.RevokeTokenAsync(request.RefreshToken);
        return Result<bool>.Success(true);
    }
}
