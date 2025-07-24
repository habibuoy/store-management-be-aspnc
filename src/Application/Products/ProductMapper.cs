using Application.Products.Create;
using Application.Products.UpdateById;
using Domain.Brands;
using Domain.Products;

namespace Application.Products;

public static class ProductMapper
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        var measureUnit = product.Detail.MeasureUnit;
        var productUnit = new ProductUnitResponse(measureUnit!.Id, measureUnit.Name.Value);
        return new ProductResponse(product.Id,
            product.Name,
            product.Detail.Description,
            product.Detail.Measure,
            productUnit,
            product.Detail.Brand!.Name,
            product.Prices.FirstOrDefault()!.Value,
            product.Tags.Select(t => t.Name.Value).ToArray(),
            product.CreatedTime, product.LastUpdatedTime
        );
    }

    public static CreateProductResponse ToCreateResponse(this Product product)
    {
        var detail = product.Detail;
        var brand = detail.Brand;
        var unit = detail.MeasureUnit;
        var price = product.Prices.FirstOrDefault();

        return new CreateProductResponse(product.Id,
            product.Name,
            detail.Description,
            detail.Measure,
            unit!.Name.Value,
            brand!.Name,
            price!.Value.ToString(),
            [.. product.Tags.Select(t => t.Name.Value)],
            product.CreatedTime,
            product.CreatedTime
        );
    }

    public static UpdateProductByIdResponse ToUpdateByIdResponse(this Product product,
        ProductPrice productPrice)
    {
        var detail = product.Detail;
        return new UpdateProductByIdResponse(product.Id,
            product.Name,
            detail.Description,
            detail.Measure,
            detail.MeasureUnit!.Name.Value,
            productPrice.Value,
            [.. product.Tags.Select(t => t.Name.Value)]
        );
    }
}