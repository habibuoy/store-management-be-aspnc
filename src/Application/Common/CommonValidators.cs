using FluentValidation;

namespace Application.Common;

public static class CommonValidators
{
    public static IRuleBuilderOptions<T, string[]>
        MustNotHaveAnyEmptyString<T>(this IRuleBuilder<T, string[]> builder)
    {
        return builder
            .Must(static (array) => !array.Any(element => string.IsNullOrEmpty(element)))
            .WithErrorCode("StringArrayWithNoEmptyElementValidator")
            .WithMessage("'{PropertyName}' should not have any element with empty or null value");
    }
}