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
    
}