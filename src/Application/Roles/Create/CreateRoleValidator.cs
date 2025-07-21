using FluentValidation;

namespace Application.Roles.Create;

internal sealed class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleValidator()
    {
        RuleFor(c => c.Role)
            .NotEmpty()
            .MinimumLength(3);
    }
}