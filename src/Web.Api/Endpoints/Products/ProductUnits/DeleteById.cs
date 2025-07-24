using Application.Abstractions.Messaging;
using Application.Products.ProductUnits.DeleteById;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products.ProductUnits;

internal sealed class DeleteById : ProductEndpoint
{
    public override IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/product-units/{id:int}", static async (
            int id,
            ICommandHandler<DeleteProductUnitByIdCommand> handler,
            CancellationToken cancellationToken
        ) =>
        {
            var command = new DeleteProductUnitByIdCommand(id);

            var result = await handler.HandleAsync(command, cancellationToken);

            return CustomHttpResults.TypedFrom(result, static () => TypedResults.Ok());
        });
        return app;
    }
}