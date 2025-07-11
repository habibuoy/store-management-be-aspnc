using FluentValidation;

namespace Application.Users.UpdateDetail;

internal sealed class UpdateUserDetailCommandValidator : AbstractValidator<UpdateUserDetailCommand>
{
    public UpdateUserDetailCommandValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .MinimumLength(3);
    }
}