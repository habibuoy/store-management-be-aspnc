using System.Reflection;
using Application.Abstractions.Messaging;
using Application.Decorators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(selector => selector.FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        if (CheckIfAssemblyImplementationOf(typeof(ICommandHandler<>)))
        {
            services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandHandler<>));
        }

        if (CheckIfAssemblyImplementationOf(typeof(ICommandHandler<,>)))
        {
            services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
        }

        return services;
    }

    private static bool CheckIfAssemblyImplementationOf(Type type)
    {
        return Assembly.GetAssembly(typeof(DependencyInjection)) is Assembly assembly
            && assembly
                .GetTypes()
                .Any(t => t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(type));
    }
}