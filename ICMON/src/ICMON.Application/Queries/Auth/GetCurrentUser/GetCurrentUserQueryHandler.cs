using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;
using ICMON.Application.Services;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Queries.Auth.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;

    public GetCurrentUserQueryHandler(IUserRepository userRepository, ICurrentUserService currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated || _currentUser.UserId == null)
            return Result<UserDto>.Failure("User is not authenticated");

        var user = await _userRepository.GetByIdWithRolesAsync(_currentUser.UserId.Value, cancellationToken);
        if (user == null)
            return Result<UserDto>.Failure("User not found");

        var permissions = await _userRepository.GetUserPermissionsAsync(user.Id, cancellationToken);
        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
        var permissionCodes = permissions.Select(p => p.Code).ToList();

        var dto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email.Value,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Status = user.Status.ToString(),
            Roles = roles,
            Permissions = permissionCodes
        };

        return Result<UserDto>.Success(dto);
    }
}
