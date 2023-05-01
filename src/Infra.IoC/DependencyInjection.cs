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
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SQL");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
            connectionString,
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
        );
        services.AddScoped<ICupomRepository, CupomRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICheckoutUseCase, CheckoutUseCase>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    public static IServiceCollection RunMgratios(this IServiceCollection services)
    {
        var context = services.BuildServiceProvider().GetService<AppDbContext>();
        context?.Database.Migrate();
        return services;
    }
}
