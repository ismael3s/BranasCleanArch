using Application;
using Application.Order.Repositories;
using Application.Order.UseCases.Checkout;
using Infra.Data.EF;
using Infra.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISomeService, SomeService>();
        return services;
    }

    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql("User ID=root;Password=root;Host=localhost;Port=5432;Database=db;", b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        services.AddScoped<ICupomRepository, CupomRepository>();
        services.AddScoped<ICheckoutUseCase, CheckoutUseCase>();
        return services;
    }

}
