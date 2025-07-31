using FluentValidation;
using Application.Common;

namespace Application.Purchases.UpdateDetailsById;

internal sealed class UpdatePurchaseDetailsByIdCommandValidator
    : AbstractValidator<UpdatePurchaseDetailsByIdCommand>
{
    public UpdatePurchaseDetailsByIdCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty();

        RuleFor(c => c.Tags)
            .MustNotHaveAnyEmptyString();

        RuleFor(c => c.OccurrenceTime)
            .NotEmpty();
    }
}