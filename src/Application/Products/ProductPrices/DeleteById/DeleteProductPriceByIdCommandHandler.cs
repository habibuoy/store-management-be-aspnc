using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Common;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Products.ProductPrices.DeleteById;

internal sealed class DeleteProductPriceByIdCommandHandler(
    IApplicationDbContext dbContext,
    ILogger<DeleteProductPriceByIdCommandHandler> logger
) : ICommandHandler<DeleteProductPriceByIdCommand>
{
    public async Task<Result> HandleAsync(DeleteProductPriceByIdCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var productPrice = await dbContext.ProductPrices
                .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

            if (productPrice == null)
            {
                return ProductErrors.PriceNotFound(command.Id);
            }

            if (productPrice.ValidToTime == null)
            {
                return ProductErrors.PriceInUse(command.Id);
            }

            dbContext.ProductPrices.Remove(productPrice);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Succeed();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "DB error has occurred while deleting product price with id '{command.Id}' from DB",
                command.Id);
            return ApplicationErrors.DBOperationError(nameof(DeleteProductPriceByIdCommandHandler),
                $"DB error has occurred while deleting product price with id '{command.Id}' from DB");
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "");
            return ApplicationErrors.OperationCancelledError(nameof(DeleteProductPriceByIdCommandHandler), ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error has occurred while deleting product price " +
                "with id '{command.Id}' from DB",
                command.Id);
            return ApplicationErrors.UnexpectedError(nameof(DeleteProductPriceByIdCommandHandler),
                "Unexpected error has occurred while deleting product price " +
                $"with id '{command.Id}' from DB");
        }
    }
}