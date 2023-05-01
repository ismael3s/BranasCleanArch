using Domain.Entities;
using UnitTests.Common;

namespace UnitTests.Domain;
internal class OrderItemFixture : BaseFixture
{
    public OrderItemFixture() : base()
    {

    }

    public OrderItem CreateValidOrderItem(int? quantity = default!, decimal? price = default!)
    {
        return new OrderItem(Guid.NewGuid(),
            price ?? Faker.Random.Decimal(1, 1000),
            quantity ?? Faker.Random.Int(1, 100)
        );
    }
}

[CollectionDefinition(nameof(OrderItemFixture))]
public class OrderItemFixtureCollection : ICollectionFixture<OrderItemFixture> { }
