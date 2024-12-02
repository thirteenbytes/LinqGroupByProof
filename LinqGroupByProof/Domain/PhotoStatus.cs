using System.ComponentModel;

namespace LinqGroupByProof.Domain;

public enum PhotoStatus
{
    [Description("Not Present")]
    NotPresent = 0,

    [Description("Pending Approval")]
    Pending = 1,

    [Description("Approved")]
    Approved = 2,

    [Description("Rejected")]
    Rejected = 3,
}
