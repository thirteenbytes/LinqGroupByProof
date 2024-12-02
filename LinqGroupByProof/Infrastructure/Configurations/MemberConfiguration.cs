using LinqGroupByProof.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinqGroupByProof.Infrastructure.Configurations;

internal class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(member => member.Id);

        builder.Property(member => member.Role)
            .HasMaxLength(25)
            .HasConversion(
                role => role.ToString(),
                value => (MemberRole)Enum.Parse(typeof(MemberRole), value));
    }
}
