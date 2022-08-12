namespace Firestone.Infrastructure.Data.Configurations;

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FireTableConfiguration : IEntityTypeConfiguration<FireTable>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<FireTable> builder)
    {
        builder.Property(table => table.Name)
               .HasMaxLength(255)
               .HasDefaultValue(string.Empty)
               .IsRequired();
    }
}
