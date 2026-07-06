using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;

namespace ICMON.Application.Queries.Auth.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<Result<UserDto>>
{
}
