using FluentValidation;

namespace ICMON.Application.Commands.Jobs.CreateJob;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
    }
}
