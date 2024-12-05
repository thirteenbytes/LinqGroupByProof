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

if (dbContext.Database.EnsureCreated())
{

    List<MemberAdded> memberAddeds = new List<MemberAdded>
{
    new MemberAdded("John Henry", MemberRole.Student, PhotoState.Activated),
    new MemberAdded("Roaxanne Smith", MemberRole.Student, PhotoState.Activated),
    new MemberAdded("Simone Juryvouski", MemberRole.Student, PhotoState.Activated),
    new MemberAdded("James Smith", MemberRole.Student, PhotoState.NotActivated),
    new MemberAdded("Hector Mac", MemberRole.Student, PhotoState.NoPhoto),
    new MemberAdded("Phil Smith", MemberRole.Student, PhotoState.Activated),
    new MemberAdded("Jane Doe", MemberRole.Staff, PhotoState.Activated),
    new MemberAdded("Bob Henry", MemberRole.Staff, PhotoState.Activated),
    new MemberAdded("Cecil Peters", MemberRole.Staff, PhotoState.NoPhoto),
};



    foreach (var memberToAdd in memberAddeds)
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
                Name = memberToAdd.Name,
                Role = memberToAdd.Role,
                MemberPhotos = memberToAdd.PhotoState == PhotoState.NoPhoto ? new List<MemberPhoto>() : memberPhotos
            });
    }

    dbContext.SaveChanges();

    var membersToUpdate = dbContext.Set<Member>().Include(m => m.MemberPhotos).ToList();

    foreach (var memberToUpdate in membersToUpdate)
    {
        var memberDetail = memberAddeds.First(m => m.Name == memberToUpdate.Name);
        if (memberDetail.PhotoState == PhotoState.Activated)
        {
            var memberPhoto = memberToUpdate.MemberPhotos.FirstOrDefault();
            memberToUpdate.ActiveMemberPhoto = memberPhoto;
            dbContext.Set<Member>().Update(memberToUpdate);
        }
    }
    dbContext.SaveChanges();
}

string query =
@"SELECT
	d.Role,
	CASE WHEN d.MemberPhotoStatus IS NULL THEN 'Missing' ELSE d.MemberPhotoStatus END AS Status,
	COUNT(d.Id) AS Count
FROM
(
	SELECT 
		m.Id,
		m.Role,
		CASE WHEN 
			m.ActiveMemberPhotoId IS NULL 
		THEN mp.Status 
		ELSE p.Status 
		END AS MemberPhotoStatus		
	FROM Members AS m
	LEFT JOIN Photos AS p ON p.Id = m.ActiveMemberPhotoId
	OUTER APPLY 
	(
		SELECT TOP 1
			p.Id,
			mp.MemberId,
			p.Status
		FROM Photos AS p 
		JOIN MemberPhotos AS mp ON mp.Id = p.Id
		WHERE mp.MemberId = m.Id
		ORDER BY CASE p.Status 
			WHEN 'Approved' THEN 1
			WHEN 'Rejected' THEN 2
			ELSE 3
		END
	) AS mp 
) AS d
GROUP BY 
	d.Role,
	d.MemberPhotoStatus";

// var formattableQuery = FormattableStringFactory.Create(query);

// var result = dbContext.Database.SqlQuery<PhotoCountResult>(formattableQuery).ToListAsync();

using var command = dbContext.Database.GetDbConnection().CreateCommand();
command.CommandText = query;

await dbContext.Database.OpenConnectionAsync();

var results = new List<PhotoCountResult>();

try
{
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        var role = reader.GetString(0);
        var status = reader.GetString(1);
        var count = reader.GetInt32(2);

        results.Add(new PhotoCountResult(role, status, count));
    }

}
finally
{
    await dbContext.Database.CloseConnectionAsync();
}

Console.WriteLine(results);

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





