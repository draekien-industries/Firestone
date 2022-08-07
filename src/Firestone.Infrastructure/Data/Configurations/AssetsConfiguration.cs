namespace Firestone.Infrastructure.Data.Configurations;

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AssetsConfiguration : IEntityTypeConfiguration<Assets>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Assets> builder)
    {
        builder.HasOne(assets => assets.AssetHolder)
               .WithMany(assetHolder => assetHolder.Assets)
               .HasForeignKey(assets => assets.AssetHolderId);

        builder.HasOne(assets => assets.LineItem)
               .WithMany(recordedLineItem => recordedLineItem.Assets)
               .HasForeignKey(assets => assets.LineItemId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
