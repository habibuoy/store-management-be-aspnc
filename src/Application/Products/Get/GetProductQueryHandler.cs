using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Products.Get;

internal sealed class GetProductQueryHandler(
    IApplicationDbContext dbContext,
    ILogger<GetProductQueryHandler> logger
) : IQueryHandler<GetProductQuery, PagedResponse<ProductResponse>>
{
    public async Task<Result<PagedResponse<ProductResponse>>> HandleAsync(GetProductQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = dbContext.Products
                .Include(p => p.Detail)
                .ThenInclude(d => d.Brand)
                .Include(p => p.Detail)
                .ThenInclude(d => d.MeasureUnit)
                .Include(p => p.Tags)
                .Include(p => p.Prices.OrderByDescending(pr => pr.CreatedTime))
                .AsSplitQuery()
                .Where(p => string.IsNullOrEmpty(query.Search) || p.Name.Contains(query.Search))
                .AsQueryable();

            result = query.SortOrder == SortOrder.ASC
                ? result.OrderBy(p => p.Name)
                : result.OrderByDescending(p => p.Name);

            var responses = result
                .Select(p => p.ToProductResponse());

            return await PagedResponse<ProductResponse>.CreateAsync(responses, query.Page, query.PageSize);
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "");
            return ApplicationErrors.OperationCancelledError(nameof(GetProductQueryHandler), ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error has occurred while querying products with '{query}'",
                query);
            return ApplicationErrors.UnexpectedError(nameof(GetProductQueryHandler),
                $"Unexpected error has occurred while querying products with '{query}'");
        }
    }
}