using Infrastructure;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Hosting.Host;

namespace Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var webHost = CreateHostBuilder(args).Build();

        await ApplyMigrations(webHost.Services);
        await webHost.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) => CreateDefaultBuilder(args).ConfigureWebHostDefaults(w => w.UseStartup<Startup>());

    private static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}