using Domain.Products;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");

        builder
            .HasMany(p => p.Tags)
            .WithMany(t => t.Products);

        builder
            .HasMany(p => p.Prices)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(p => p.Detail)
            .WithOne(pd => pd.Product)
            .HasForeignKey<ProductDetail>(pd => pd.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
{
    public void Configure(EntityTypeBuilder<ProductDetail> builder)
    {
        builder.ToTable("product_detail");

        builder
            .HasKey(pd => pd.ProductId);

        builder
            .HasOne(pd => pd.Brand)
            .WithMany()
            .HasForeignKey(pd => pd.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(pd => pd.MeasureUnit)
            .WithMany(pu => pu.Products)
            .HasForeignKey(pd => pd.MeasureUnitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.ToTable("product_price");
    }
}

internal sealed class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.ToTable("product_tag");

        builder
            .OwnsOne(pt => pt.Name)
            .ConfigureNameProperty();
    }
}

internal sealed class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
{
    public void Configure(EntityTypeBuilder<ProductUnit> builder)
    {
        builder.ToTable("product_unit");

        builder
            .OwnsOne(pu => pu.Name)
            .ConfigureNameProperty();
    }
}