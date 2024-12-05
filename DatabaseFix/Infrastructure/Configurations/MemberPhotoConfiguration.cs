using DatabaseFix.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseFix.Infrastructure.Configurations;

internal class MemberPhotoConfiguration : IEntityTypeConfiguration<MemberPhoto>
{
    public void Configure(EntityTypeBuilder<MemberPhoto> builder)
    {
        builder.ToTable("MemberPhotos");
        builder.HasOne(photo => photo.ActivePhotoMember).WithOne(member => member.ActiveMemberPhoto).HasForeignKey<Member>("ActiveMemberPhotoId");
    }
}
