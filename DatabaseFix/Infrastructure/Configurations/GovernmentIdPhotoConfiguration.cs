using DatabaseFix.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseFix.Infrastructure.Configurations;

internal class GovernmentIdPhotoConfiguration : IEntityTypeConfiguration<GovernmentIdPhoto>
{
    public void Configure(EntityTypeBuilder<GovernmentIdPhoto> builder)
    {
        builder.ToTable("GovermentIdPhotos");        
        builder.Property(photo => photo.Type)
            .HasMaxLength(25)
            .HasConversion(
                role => role.ToString(),
                value => Enum.Parse<GovernmentIdPhotoType>(value));
    }
}
