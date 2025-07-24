using Application.Abstractions.Messaging;
using Application.Common;

namespace Application.Products.Get;

public sealed record GetProductQuery(string? Search, SortOrder SortOrder,
    int Page = 1, int PageSize = 10)
    : IQuery<PagedResponse<ProductResponse>>;