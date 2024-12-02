namespace LinqGroupByProof.Domain;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MemberRole Role { get; set; }
    public List<MemberPhoto> MemberPhotos { get; set; } = new();
    public MemberPhoto? ActiveMemberPhoto { get; set; } = null!;
}
