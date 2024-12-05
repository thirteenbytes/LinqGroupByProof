using DatabaseFix.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseFix.Infrastructure.Configurations;

internal class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("Photos");

        builder.HasKey(photo => photo.Id);



        builder.Property(photo => photo.PhotoType)
            .HasMaxLength(25)
            .HasConversion(type =>
                type.ToString(),
                value => Enum.Parse<PhotoType>(value));

        builder.Property(memberPhoto => memberPhoto.Status)
            .HasMaxLength(25)
            .HasConversion(status =>
                status.ToString(),
                value => Enum.Parse<PhotoStatus>(value));

    }
}
