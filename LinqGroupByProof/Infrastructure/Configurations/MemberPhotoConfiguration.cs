using LinqGroupByProof.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinqGroupByProof.Infrastructure.Configurations;

internal class MemberPhotoConfiguration : IEntityTypeConfiguration<MemberPhoto>
{
    public void Configure(EntityTypeBuilder<MemberPhoto> builder)
    {
        builder.ToTable("MemberPhotos");
        builder.HasOne(photo => photo.Member).WithMany(member => member.MemberPhotos);
        builder.HasOne(photo => photo.ActivePhotoMember).WithOne(member => member.ActiveMemberPhoto).HasForeignKey<Member>("ActiveMemberPhotoId");
    }
}
