using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;
using ICMON.Application.Services;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameOrEmailAsync(request.UsernameOrEmail, cancellationToken);
        if (user == null)
            return Result<LoginResponse>.Failure("Invalid username/email or password");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Result<LoginResponse>.Failure("Invalid username/email or password");

        if (user.Status != Domain.Enums.UserStatus.Active)
            return Result<LoginResponse>.Failure("Account is not active");

        user.RecordLogin();
        _userRepository.Update(user);

        var permissions = await _userRepository.GetUserPermissionsAsync(user.Id, cancellationToken);
        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
        var permissionCodes = permissions.Select(p => p.Code).ToList();

        var response = await _tokenService.GenerateTokensAsync(user.Id, user.Username, roles, permissionCodes);
        return Result<LoginResponse>.Success(response);
    }
}
