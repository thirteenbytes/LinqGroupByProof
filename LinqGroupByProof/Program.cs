using LinqGroupByProof.Domain;
using LinqGroupByProof.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        // Get the connection string from appsettings.json
        var connectionString = context.Configuration.GetConnectionString("Current");

        // Register DbContext with the connection string
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    })
    .Build();


using var scope = host.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//Console.WriteLine("Ensuring database is created...");
//dbContext.Database.EnsureCreated();
//Console.WriteLine("Database created!");


List<MemberAdded> memberAddeds = new List<MemberAdded>
{
    new MemberAdded("John Henry", MemberRole.Student),
    new MemberAdded("Roaxanne Smith", MemberRole.Student),
    new MemberAdded("Simone Juryvouski", MemberRole.Student),
    new MemberAdded("James Smith", MemberRole.Student),
    new MemberAdded("Hector Mac", MemberRole.Student),
    new MemberAdded("Phil Smith", MemberRole.Student),
    new MemberAdded("Jane Doe", MemberRole.Staff),
    new MemberAdded("Bob Henry", MemberRole.Staff),
    new MemberAdded("Cecil Peters", MemberRole.Staff),
};


var memberPhotos = new List<MemberPhoto>
{
    new MemberPhoto
    {
        Id = Guid.NewGuid(),
        DateTaken = DateTime.Now,
        PhotoType = PhotoType.Member,
        Status = PhotoStatus.Approved
    }
};

foreach (var memberToAdd in memberAddeds)
{
    dbContext.Set<Member>().Add(
        new Member
        {
            Id = Guid.NewGuid(),
            Name = memberToAdd.name,
            Role = memberToAdd.role,
            MemberPhotos = memberPhotos,
        });
}

dbContext.SaveChanges();

var memberPhoto = dbContext.Set<MemberPhoto>().First();
var member = dbContext.Set<Member>().First();

member.ActiveMemberPhoto = memberPhoto;

dbContext.Set<Member>().Update(member);
dbContext.SaveChanges();

record MemberAdded(string name, MemberRole role);

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



