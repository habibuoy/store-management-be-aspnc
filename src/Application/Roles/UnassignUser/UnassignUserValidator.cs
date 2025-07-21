using FluentValidation;

namespace Application.Roles.UnassignUser;

internal sealed class UnassignUserValidator : AbstractValidator<UnassignUserCommand>
{
    public UnassignUserValidator()
    {
        RuleFor(c => c.User)
            .NotEmpty();
    }
}