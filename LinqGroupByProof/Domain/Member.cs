namespace LinqGroupByProof.Domain;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MemberRole Role { get; set; }
    public List<MemberPhoto> MemberPhotos { get; protected set; } = new();
    public MemberPhoto? ActiveMemberPhoto { get; protected set; } = null!;
}
