namespace DatabaseFix.Domain;
public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MemberRole Role { get; set; }
    public List<Photo> Photos { get; set; } = new();
    public MemberPhoto? ActiveMemberPhoto { get; set; } = null!;
}
