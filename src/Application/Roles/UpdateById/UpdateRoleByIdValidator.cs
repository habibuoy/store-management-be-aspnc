using FluentValidation;

namespace Application.Roles.UpdateById;

internal sealed class UpdateRoleByIdValidator
    : AbstractValidator<UpdateRoleByIdCommand>
{
    public UpdateRoleByIdValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MinimumLength(3);
    }
}