using DatabaseFix.Application;
using DatabaseFix.Domain;
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

if (dbContext.Database.EnsureCreated())
{
    List<SampleMember> sampleMembers = new List<SampleMember>
    {
        new SampleMember("John Henry", MemberRole.Student, SamplePhotoState.Activated),
        new SampleMember("Roaxanne Smith", MemberRole.Student, SamplePhotoState.Activated),
        new SampleMember("Simone Juryvouski", MemberRole.Student, SamplePhotoState.Activated),
        new SampleMember("James Smith", MemberRole.Student, SamplePhotoState.NotActivated),
        new SampleMember("Hector Mac", MemberRole.Student, SamplePhotoState.NoPhoto),
        new SampleMember("Phil Smith", MemberRole.Student, SamplePhotoState.Activated),
        new SampleMember("Jane Doe", MemberRole.Staff, SamplePhotoState.Activated),
        new SampleMember("Bob Henry", MemberRole.Staff, SamplePhotoState.Activated),
        new SampleMember("Cecil Peters", MemberRole.Staff, SamplePhotoState.NoPhoto),
    };


    foreach (var sampleMember in sampleMembers)
    {
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

        dbContext.Set<Member>().Add(
            new Member
            {
                Id = Guid.NewGuid(),
                Name = sampleMember.Name,
                Role = sampleMember.Role,
                Photos = sampleMember.PhotoState == SamplePhotoState.NoPhoto ? new List<Photo>() : memberPhotos.Cast<Photo>().ToList()
            });

        dbContext.SaveChanges();

        var membersToUpdate = dbContext.Set<Member>().Include(m => m.Photos).ToList();

        foreach (var memberToUpdate in membersToUpdate)
        {
            var memberDetail = sampleMembers.First(m => m.Name == memberToUpdate.Name);
            if (memberDetail.PhotoState == SamplePhotoState.Activated)
            {
                var memberPhoto = memberToUpdate.Photos.FirstOrDefault();
                memberToUpdate.ActiveMemberPhoto = memberPhoto as MemberPhoto;
                dbContext.Set<Member>().Update(memberToUpdate);
            }
        }
        dbContext.SaveChanges();
    }

}

var result = dbContext.Set<Member>()
    .Select(m => new
    {
        m.Id,
        m.Role,
        MemberPhoto = m.ActiveMemberPhoto != null ? m.ActiveMemberPhoto : m.Photos.OrderBy(p => p.Status == PhotoStatus.Approved ? 0 :
                                                                                                p.Status == PhotoStatus.Rejected ? 1 : 2)
                                                                                    .ThenByDescending(p => p.DateTaken).FirstOrDefault()
    })
    .Select(m => new
    {
        m.Id,
        m.Role,
        MemberPhotoStatus = m.MemberPhoto != null ? m.MemberPhoto.Status : PhotoStatus.NotPresent

    })
    .GroupBy(g => new { g.Role, g.MemberPhotoStatus })
    .Select(g => new
    {
        g.Key.Role,
        g.Key.MemberPhotoStatus,
        Count = g.Count()
    });

Console.WriteLine(result.ToQueryString());

