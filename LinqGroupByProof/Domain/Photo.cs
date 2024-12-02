namespace LinqGroupByProof.Domain;

public class Photo
{
    public Guid Id { get; set; }
    public DateTime DateTaken { get; set; }
    public PhotoStatus Status { get; set; }
    public Member Member { get; set; } = null!;

}
