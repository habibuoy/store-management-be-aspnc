using Application.Abstractions.Messaging;
using Application.Products.Create;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Infrastructure;
using Web.Api.Common;

namespace Web.Api.Endpoints.Products;

internal sealed class Create : ProductEndpoint
{
    public sealed record CreateProductRequest(string Name, string? Description, int BrandId,
        float Measure, int MeasureUnitId, decimal Price, string[] Tags);

    public override IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/", static async (
            HttpContext httpContext,
            [FromBody] CreateProductRequest request,
            ICommandHandler<CreateProductCommand, CreateProductResponse> handler,
            CancellationToken cancellationToken
        ) =>
        {
            var command = new CreateProductCommand(
                request.Name, request.Description, request.Measure, request.MeasureUnitId, request.BrandId,
                request.Price, request.Tags
            );

            var result = await handler.HandleAsync(command, cancellationToken);

            return CustomHttpResults.TypedFrom(result,
                static (r, ctx) => TypedResults.Created($"{ctx.ToUriFullAbsolutePath()}/{r.Id}", r),
                httpContext);
        });
        return app;
    }
}