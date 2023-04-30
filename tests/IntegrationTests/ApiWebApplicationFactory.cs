using Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace IntegrationTests;
public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddTransient<ISomeService, TesteService>();
        });
    }
}

public class TesteService : ISomeService
{
    public void Do()
    {
        Debug.WriteLine("Test Code");
    }
}
