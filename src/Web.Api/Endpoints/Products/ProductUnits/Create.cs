using Application.Abstractions.Messaging;
using Application.Products.ProductUnits.Create;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products.ProductUnits;

internal sealed class Create : ProductEndpoint
{
    public sealed record CreateProductUnitRequest(string Name);

    public override IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/product-units", static async (
            HttpContext httpContext,
            [FromBody] CreateProductUnitRequest request,
            ICommandHandler<CreateProductUnitCommand, CreateProductUnitResponse> handler,
            CancellationToken cancellationToken
        ) =>
        {
            var command = new CreateProductUnitCommand(request.Name);

            var result = await handler.HandleAsync(command, cancellationToken);

            return CustomHttpResults.TypedFrom(result,
                static (r, ctx) => TypedResults.Created($"{ctx!.Request.Host.Value}/products/product-units/{r.Id}",
                    r), httpContext);
        });
        return app;
    }
}