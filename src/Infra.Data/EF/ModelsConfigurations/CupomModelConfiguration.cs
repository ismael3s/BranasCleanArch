using Infra.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EF.ModelsConfigurations;
public class CupomModelConfiguration : IEntityTypeConfiguration<Cupom>
{
    public void Configure(EntityTypeBuilder<Cupom> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(c => c.Code).IsRequired();
        builder.Property(c => c.Discount).IsRequired().HasPrecision(10, 2);
    }
}
