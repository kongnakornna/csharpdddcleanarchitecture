using FluentValidation;

namespace ICMON.Application.Commands.Jobs.UpdateJobStatus;

public class UpdateJobStatusCommandValidator : AbstractValidator<UpdateJobStatusCommand>
{
    public UpdateJobStatusCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
        RuleFor(x => x.NewStatus).IsInEnum();
    }
}
