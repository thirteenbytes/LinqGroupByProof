using System.ComponentModel;

namespace LinqGroupByProof.Domain;

public enum MemberRole
{
    [Description("Student")]
    Student = 1,

    [Description("Staff")]
    Staff = 2
}