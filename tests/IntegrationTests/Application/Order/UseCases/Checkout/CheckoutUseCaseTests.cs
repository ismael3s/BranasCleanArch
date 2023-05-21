using Application.Order.UseCases.Checkout;
using FluentAssertions;
using Infra.Data.EF;
using Infra.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Application.Order.UseCases.Checkout;
public class CheckoutUseCaseTests
{
    [Fact]
    [Trait("Integration/Application", "Checkout - UseCase")]
    public async Task DeveCriarUmPedidoComTresItems()
    {
        var productId = Guid.NewGuid();
        var checkoutInputItems = new List<CheckoutInputItemDto>() { new(productId.ToString(), 1) };
        var checkoutInputDTO = new CheckoutInputDto("072.429.275-60", checkoutInputItems);

        var context = new AppDbContext(
             new DbContextOptionsBuilder<AppDbContext>()
             .UseNpgsql("User ID=root;Password=root;Host=localhost;Port=5432;Database=branas_db;")
             .Options
         );

        await context.Database.EnsureCreatedAsync();

        await context.Database.BeginTransactionAsync();

        await context.Products.AddAsync(new Infra.Data.EF.Models.Product()
        {
            Id = productId,
            Name = "Produto 1",
            Price = 200M
        });

        await context.SaveChangesAsync();

        var uow = new UnitOfWork(context);
        var couponRepository = new CouponRepository(context);
        var orderRepository = new OrderRepository(context);
        var productRepository = new ProductRepository(context);

        var sut = new CheckoutUseCase(couponRepository, orderRepository, uow, productRepository);

        var result = await sut.Handle(checkoutInputDTO, CancellationToken.None);

        await context.Database.RollbackTransactionAsync();


        result.Total.Should().Be(200M);
    }
}
