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
        var connectionString = context.Configuration.GetConnectionString("Main");

        // Register DbContext with the connection string
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    })
    .Build();


using var scope = host.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

Console.WriteLine("Ensuring database is created...");
dbContext.Database.EnsureCreated();
Console.WriteLine("Database created!");


//var memberPhotos = new List<MemberPhoto>
//{
//    new MemberPhoto 
//    { 
//        Id = Guid.NewGuid(), 
//        DateTaken = DateTime.Now, 
//        PhotoType = PhotoType.Member, 
//        Status = PhotoStatus.Approved 
//    }
//};

//dbContext.Set<Member>().Add(
//    new Member 
//    { 
//        Id = Guid.NewGuid(), 
//        Name = "John Doe", 
//        Role = MemberRole.Student, 
//        MemberPhotos = memberPhotos,        
//    });

//dbContext.SaveChanges();

//var memberPhoto = dbContext.Set<MemberPhoto>().First();
//var member = dbContext.Set<Member>().First();

//member.ActiveMemberPhoto = memberPhoto;

//dbContext.Set<Member>().Update(member);
//dbContext.SaveChanges();


//var result = dbContext.Set<Member>()
//    .Select(m => new
//    {

//    })