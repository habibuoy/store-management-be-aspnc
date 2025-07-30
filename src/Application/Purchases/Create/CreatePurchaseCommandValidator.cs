using FluentValidation;
using Application.Common;

namespace Application.Purchases.Create;

internal sealed class CreatePurchaseCommandValidator
    : AbstractValidator<CreatePurchaseCommand>
{
    public CreatePurchaseCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty();

        RuleFor(c => c.Tags)
            .MustNotHaveAnyEmptyString();

        RuleFor(c => c.ProductEntries)
            .Cascade(CascadeMode.Stop)
            .MustNotHaveAnyNullElement()
            .Must((_, entries, ctx) =>
            {
                bool isValid = true;

                for (int i = 0; i < entries.Count; i++)
                {
                    var entry = entries[i];
                    if (entry.Quantity < 1)
                    {
                        ctx.AddFailure($"Product entry's 'Amount' at index {i} for product id '{entry.Id}' " +
                            "should be at least 1");
                        isValid = false;
                    }
                }

                return isValid;
            })
                .WithErrorCode("ProductEntriesValidator")
                .WithMessage("Invalid Product Entries");

        RuleFor(c => c.OccurrenceTime)
            .NotEmpty();
    }
}