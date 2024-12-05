public enum PhotoState
{
    NoPhoto, NotActivated, Activated
}

//var result = dbContext.Set<Member>()
//    .Select(m => new
//    {
//        m.Id,
//        m.Role,
//        MemberPhoto = m.ActiveMemberPhoto != null ? m.ActiveMemberPhoto : 
//        m.MemberPhotos
//        .OrderBy(mp =>
//            mp.Status == PhotoStatus.Approved ? 0 : 
//            mp.Status == PhotoStatus.Rejected ? 1 : 2)
//        .ThenByDescending(mp => mp.DateTaken)
//        .FirstOrDefault()
//    })
//    .Select(m=> new
//    {
//        m.Id,
//        m.Role,
//        MemberPhotoStatus = m.MemberPhoto != null ? m.MemberPhoto.Status : PhotoStatus.NotPresent
//    })
//    .GroupBy(g=> new {g.Role, g.MemberPhotoStatus})
//    .Select(g => new
//    {
//        g.Key.Role,
//        g.Key.MemberPhotoStatus,
//        Count = g.Count()
//    });

//Console.WriteLine(result.ToQueryString());





