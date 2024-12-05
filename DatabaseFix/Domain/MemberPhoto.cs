namespace DatabaseFix.Domain;

public class MemberPhoto : Photo
{
    public Member? ActivePhotoMember { get; set; } = null!;
}
