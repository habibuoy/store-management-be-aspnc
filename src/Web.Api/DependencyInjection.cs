using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using Web.Api.Middlewares;

namespace Web.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenWithAuth();

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        });

        return services;
    }

    private static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services
            .AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(id => id.FullName!.Replace('+', '.'));

                var securityScheme = new OpenApiSecurityScheme()
                {
                    Name = "JWT Authentication",
                    Description = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                };

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            }
                        },
                        []
                    }
                };

                options.AddSecurityRequirement(securityRequirement);
            });

        return services;
    }
}