using Application.Abstractions.Messaging;
using Application.Users.UpdateDetail;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class UpdateDetail : UserEndpoint
{
    public sealed record UpdateDetailRequest(string FirstName, string? LastName);

    public override IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}/details", static async (Guid id, [FromBody] UpdateDetailRequest request,
            ICommandHandler<UpdateUserDetailCommand, UpdateUserDetailResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateUserDetailCommand(id, request.FirstName, request.LastName);

            var result = await handler.HandleAsync(command, cancellationToken);

            return CustomHttpResults.TypedFrom(result, static r => TypedResults.Ok(r));
        })
            .RequireAuthorization()
            ;

        return app;
    }
}