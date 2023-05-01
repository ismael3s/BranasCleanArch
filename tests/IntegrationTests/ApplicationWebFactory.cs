using Infra.Data.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace IntegrationTests;
public class ApplicationWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();
    private HttpClient? _httpClient;
    public HttpClient HttpClient => _httpClient;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            var teste = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            services.Remove(teste!);
            services.AddSingleton<DbConnection>(_ =>
                 new NpgsqlConnection(_postgreSqlContainer.GetConnectionString())
            );

            services.AddDbContext<AppDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();

                Debug.WriteLine(connection.ConnectionString);
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });
        });
    }

    public async Task SeedWithAsync(Func<AppDbContext, Task> action)
    {
        var scopeFactory = this.Services.GetService<IServiceScopeFactory>();

        using var scope = scopeFactory!.CreateScope();
        var db = scope.ServiceProvider.GetService<AppDbContext>();


        if (db is not null && action is not null)
        {
            await db.Database.EnsureCreatedAsync();
            db.Products.RemoveRange(db.Products);
            db.OrderItems.RemoveRange(db.OrderItems);
            await db.SaveChangesAsync();
            await action(db);
        }
    }


    Task IAsyncLifetime.DisposeAsync()
    {
        return _postgreSqlContainer.DisposeAsync().AsTask();

    }

    public Task ResetDatabaseAsync()
        => Task.CompletedTask;
    //=> await Respawner.ResetAsync(DbConnection);

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        _httpClient = CreateClient();
        //await InitializeRespawner();
    }

    //private async Task InitializeRespawner()
    //{
    //    await DbConnection.OpenAsync();
    //    _respawner = await Respawner.CreateAsync(DbConnection, new RespawnerOptions
    //    {
    //        DbAdapter = DbAdapter.Postgres,
    //        SchemasToInclude = new[] { "public" }
    //    });
    //}

    public new async Task DisposeAsync()
        => await _postgreSqlContainer.DisposeAsync();
}

