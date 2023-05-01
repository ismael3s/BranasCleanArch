using Application.Order.Repositories;
using Application.Order.UseCases.Checkout;
using Domain.Entities;
using Moq;
using UnitTests.Common;

namespace UnitTests.Application.Order.UseCases;
public class CheckoutFixture : BaseFixture
{
    public CheckoutFixture() : base()
    {

    }
    public Mock<IOrderRepository> GetOrderRepository() => new();
    public Mock<ICouponRepository> GetCupomRepository() => new();
    public Mock<IProductRepository> GetProductRepository() => new();
    public Mock<IUnitOfWork> GetUnitOfWork() => new();

    public CheckoutInputItemDto CreateValidCheckoutInputItemDto(int quantity)
    {
        return new CheckoutInputItemDto(Guid.NewGuid().ToString(), quantity);
    }

    public Product CreateProduct(decimal? price = default!)
    {
        return Product.Create(Faker.Commerce.ProductName(), price ?? Faker.Random.Decimal(1, 1000));
    }
}

[CollectionDefinition(nameof(CheckoutFixture))]
public class CheckoutFixtureCollection : ICollectionFixture<CheckoutFixture> { }
