using Application.Products.Create;
using Application.Products.ProductTags;
using Application.Products.ProductTags.Create;
using Application.Products.ProductTags.UpdateById;
using Application.Products.ProductUnits;
using Application.Products.ProductUnits.Create;
using Application.Products.ProductUnits.UpdateById;
using Application.Products.UpdateById;
using Domain.Products;

namespace Application.Products;

public static class ProductMapper
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        var measureUnit = product.Detail.MeasureUnit;
        var productUnit = new MeasureUnitResponse(measureUnit!.Id, measureUnit.Name.Value);
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

    public static CreateProductUnitResponse ToCreateResponse(this ProductUnit productUnit)
    {
        return new CreateProductUnitResponse(productUnit.Id, productUnit.Name.Value,
            productUnit.Name.Normalized);
    }

    public static ProductUnitResponse ToProductUnitResponse(this ProductUnit productUnit)
    {
        return new ProductUnitResponse(productUnit.Id, productUnit.Name.Value,
            productUnit.Name.Normalized);
    }

    public static UpdateProductUnitByIdResponse ToUpdateByIdResponse(this ProductUnit productUnit)
    {
        return new UpdateProductUnitByIdResponse(productUnit.Id, productUnit.Name.Value,
            productUnit.Name.Normalized);
    }

    public static CreateProductTagResponse ToCreateResponse(this ProductTag productUnit)
    {
        return new CreateProductTagResponse(productUnit.Id, productUnit.Name.Value,
            productUnit.Name.Normalized);
    }

    public static ProductTagResponse ToProductTagResponse(this ProductTag productUnit)
    {
        return new ProductTagResponse(productUnit.Id, productUnit.Name.Value,
            productUnit.Name.Normalized);
    }

    public static UpdateProductTagByIdResponse ToUpdateByIdResponse(this ProductTag productUnit)
    {
        return new UpdateProductTagByIdResponse(productUnit.Id, productUnit.Name.Value,
            productUnit.Name.Normalized);
    }
}