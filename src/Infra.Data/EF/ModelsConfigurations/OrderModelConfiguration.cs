using Infra.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EF.ModelsConfigurations;
public class OrderModelConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Total).IsRequired().HasPrecision(10, 2);
        builder.Property(o => o.Cpf).IsRequired();
        builder.HasOne(o => o.Cupom).WithOne(c => c.Order).HasForeignKey<Order>(o => o.CupomId).IsRequired(false);
    }
}
