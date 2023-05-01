using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain;

[Collection(nameof(OrderTestFixture))]
public class OrderTest
{
    private readonly OrderTestFixture _orderTestFixture;

    public OrderTest(OrderTestFixture orderTestFixture)
    {
        _orderTestFixture = orderTestFixture;
    }

    [Fact(DisplayName = nameof(DeveCriarUmPedidoValidoComTresProdutosECalcularOValorTotal))]
    [Trait("Domain", "Order")]
    public void DeveCriarUmPedidoValidoComTresProdutosECalcularOValorTotal()
    {
        var order = Order.Create(_orderTestFixture.RandomValidCpf());
        order.AddItem(_orderTestFixture.CreateValidOrderItem(100, 1));
        order.AddItem(_orderTestFixture.CreateValidOrderItem(100, 1));
        order.AddItem(_orderTestFixture.CreateValidOrderItem(100, 1));

        var total = order.CalculateTotal();

        total.Should().Be(300);
    }


    [Fact(DisplayName = nameof(DeveCriarUmPedidoComTresItems_AplicarUmCupomDeDesconto_ECalcularOValorTotal))]
    [Trait("Domain", "Order")]
    public void DeveCriarUmPedidoComTresItems_AplicarUmCupomDeDesconto_ECalcularOValorTotal()
    {
        var order = Order.Create(_orderTestFixture.RandomValidCpf());
        order.AddItem(_orderTestFixture.CreateValidOrderItem(100, 1));
        order.AddItem(_orderTestFixture.CreateValidOrderItem(100, 1));
        order.AddItem(_orderTestFixture.CreateValidOrderItem(100, 1));
        order.ApplyCupom(Cupom.Create("VALE20", 20));

        var total = order.CalculateTotal();

        total.Should().Be(240);
    }

    [Fact(DisplayName = nameof(NaoDeveSerPossivelAplicarUmCupom_QuandoEleENullo))]
    [Trait("Domain", "Order")]
    public void NaoDeveSerPossivelAplicarUmCupom_QuandoEleENullo()
    {
        var action = () =>
        {
            var order = Order.Create(_orderTestFixture.RandomValidCpf());
            order.ApplyCupom(null!);
        };

        action.Should().Throw<ArgumentException>()
            .And.Message.Contains("inexistente");
    }

}
