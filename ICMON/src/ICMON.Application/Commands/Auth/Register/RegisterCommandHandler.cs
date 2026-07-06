using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Auth;
using ICMON.Application.Services;
using ICMON.Domain.Entities;
using ICMON.Domain.Interfaces;
using ICMON.Domain.ValueObjects;

namespace ICMON.Application.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var usernameTaken = await _userRepository.IsUsernameTakenAsync(request.Username, cancellationToken);
        if (usernameTaken)
            return Result<RegisterResponse>.Failure("Username is already taken");

        var emailTaken = await _userRepository.IsEmailTakenAsync(request.Email, cancellationToken);
        if (emailTaken)
            return Result<RegisterResponse>.Failure("Email is already registered");

        var email = new Email(request.Email);
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = User.Create(
            request.Username,
            email,
            passwordHash,
            request.FirstName,
            request.LastName,
            Guid.Empty // whitelabelId - will be set by admin
        );

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new RegisterResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email.Value,
            Message = "User registered successfully"
        };

        return Result<RegisterResponse>.Success(response);
    }
}
