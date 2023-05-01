using Infra.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EF.ModelsConfigurations;
public class OrderItemModelConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Price).IsRequired().HasPrecision(10, 2);
        builder.Property(o => o.Quantity).IsRequired();
        builder.Property(o => o.ProductName).IsRequired();
        builder.Property(o => o.OrderId).IsRequired();

        builder.HasOne(o => o.Order).WithMany(o => o.OrderItems).HasForeignKey(o => o.OrderId);
    }
}
