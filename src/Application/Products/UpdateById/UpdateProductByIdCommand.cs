using Application.Abstractions.Messaging;

namespace Application.Products.UpdateById;

public sealed record UpdateProductByIdCommand(Guid Id,
    string Name, string? Description, float Measure,
    int UnitId, decimal Price, string[] Tags
) : ICommand<UpdateProductByIdResponse>;