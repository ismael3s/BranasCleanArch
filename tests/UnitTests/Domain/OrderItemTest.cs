using Bogus;
using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain;
public class OrderItemTest
{
    private readonly Faker _faker = new Faker("pt_BR");

    [Fact(DisplayName = nameof(DeveSerPossivelCriarUmaOrdemItemQuandoOsDadosForemValidos))]
    [Trait("Domain", "OrderItem")]
    public void DeveSerPossivelCriarUmaOrdemItemQuandoOsDadosForemValidos()
    {
        var orderItem = new OrderItem(_faker.Commerce.ProductName(), _faker.Random.Decimal(), _faker.Random.Number(1, 10));

        orderItem.Should().NotBeNull();
        orderItem.Should().BeOfType<OrderItem>();
    }

    [Fact(DisplayName = nameof(NaoDeveSerPossivelCriarUmOrderItem_QuandoAQuantidadeEMenorQueUm))]
    [Trait("Domain", "OrderItem")]
    public void NaoDeveSerPossivelCriarUmOrderItem_QuandoAQuantidadeEMenorQueUm()
    {
        var action = () => new OrderItem(_faker.Commerce.ProductName(), _faker.Random.Decimal(), _faker.Random.Number(-100, 0));

        action.Should().Throw<ArgumentException>()
            .WithMessage("Quantidade não pode ser menor do que um");
    }

    [Fact(DisplayName = nameof(NaoDeveSerPossivelCriarUmOrderItem_QuandoOPrecoEMenorQueZero))]
    [Trait("Domain", "OrderItem")]
    public void NaoDeveSerPossivelCriarUmOrderItem_QuandoOPrecoEMenorQueZero()
    {
        var action = () => new OrderItem(_faker.Commerce.ProductName(), _faker.Random.Decimal(-10.00M, -0.1M), 1);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Preço não pode ser menor do que zero");
    }
}
