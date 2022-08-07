namespace Firestone.Infrastructure.Data.Configurations;

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasOne(lineItem => lineItem.FireTable)
               .WithMany(table => table.LineItems)
               .HasForeignKey(lineItem => lineItem.FireTableId);
    }
}
