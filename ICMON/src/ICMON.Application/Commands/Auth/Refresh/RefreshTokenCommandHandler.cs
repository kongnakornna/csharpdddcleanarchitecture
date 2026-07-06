using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;
using ICMON.Application.Services;

namespace ICMON.Application.Commands.Auth.Refresh;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
{
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _tokenService.RefreshTokenAsync(request.RefreshToken);
            return Result<RefreshTokenResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<RefreshTokenResponse>.Failure(ex.Message);
        }
    }
}
