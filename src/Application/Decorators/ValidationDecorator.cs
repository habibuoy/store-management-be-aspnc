using Application.Abstractions.Messaging;
using FluentValidation;
using FluentValidation.Results;
using Shared;

namespace Application.Decorators;

internal static class ValidationDecorator
{
    internal sealed class CommandHandler<TCommand>(
        ICommandHandler<TCommand> handler,
        IEnumerable<IValidator<TCommand>> validators) 
            : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var errors = await ValidateAsync(command, validators, cancellationToken);
            if (errors.Length > 0)
            {
                return CreateValidationError(errors);
            }

            return await handler.HandleAsync(command, cancellationToken);
        }
    }

    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> handler,
        IEnumerable<IValidator<TCommand>> validators) 
            : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var errors = await ValidateAsync(command, validators, cancellationToken);
            if (errors.Length > 0)
            {
                return CreateValidationError(errors);
            }

            return await handler.HandleAsync(command, cancellationToken);
        }
    }

    private static async Task<ValidationFailure[]> ValidateAsync<TCommand>(TCommand command,
        IEnumerable<IValidator<TCommand>> validators,
        CancellationToken cancellationToken)
    {
        if (validators == null
            || !validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TCommand>(command);

        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        return [.. results
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)];
    }

    private static ValidationError CreateValidationError(ValidationFailure[] errors)
    {
        return ValidationError.From(errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => Error.Problem(e.ErrorCode, e.ErrorMessage)).ToList()
            ));
    }
}