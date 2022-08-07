namespace Firestone.Infrastructure.Data.Configurations;

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AssetHolderConfiguration : IEntityTypeConfiguration<AssetHolder>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AssetHolder> builder)
    {
        builder.HasOne(assetHolder => assetHolder.FireTable)
               .WithMany(fireTable => fireTable.AssetHolders)
               .HasForeignKey(assetHolder => assetHolder.FireTableId);

        builder.Property(x => x.Name)
               .HasMaxLength(250)
               .IsUnicode(false);
    }
}
