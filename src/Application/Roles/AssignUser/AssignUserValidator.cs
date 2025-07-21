using FluentValidation;

namespace Application.Roles.AssignUser;

internal sealed class AssignUserValidator : AbstractValidator<AssignUserCommand>
{
    public AssignUserValidator()
    {
        RuleFor(c => c.User)
            .NotEmpty();
    }
}