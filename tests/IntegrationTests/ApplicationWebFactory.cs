using Infra.Data.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;
public class ApplicationWebFactory : WebApplicationFactory<Program>
{
    public HttpClient HttpClient { get; private set; } = default!;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureTestServices(services =>
        {
            var teste = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            services.Remove(teste!);
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });
        });
    }
}

