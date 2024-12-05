namespace DatabaseFix.Domain;

public class Photo
{
    public Guid Id { get; set; }
    public PhotoType PhotoType { get; set; }
    public DateTime DateTaken { get; set; }
    public PhotoStatus Status { get; set; }
    public Member Member { get; set; } = null!;
}
