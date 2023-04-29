using Domain.Entities;
using Domain.VO;
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
        order.AddItem(new OrderItem("Produto 1", 100, 1));
        order.AddItem(item: new OrderItem("Produto 2", 100, 1));
        order.AddItem(new OrderItem("Produto 3", 100, 1));

        var total = order.CalculateTotal();

        total.Should().Be(300);
    }


    [Fact(DisplayName = nameof(DeveCriarUmPedidoComTresItems_AplicarUmCupomDeDesconto_ECalcularOValorTotal))]
    [Trait("Domain", "Order")]
    public void DeveCriarUmPedidoComTresItems_AplicarUmCupomDeDesconto_ECalcularOValorTotal()
    {
        var order = Order.Create(_orderTestFixture.RandomValidCpf(), new Cupom("VALE20", 20m));
        order.AddItem(new OrderItem("Produto 1", 100, 1));
        order.AddItem(new OrderItem("Produto 2", 100, 1));
        order.AddItem(new OrderItem("Produto 3", 100, 1));

        var total = order.CalculateTotal();

        total.Should().Be(240);
    }

    [Fact(DisplayName = nameof(DeveCriarUmPedidoComTresItems_AplicarUmCupomDeDesconto_ECalcularOValorTotal))]
    [Trait("Domain", "Order")]
    public void DeveCriarUmPedidoComTresItems_AplicarUmCupomDeDescontoDeZeroPorcento_ECalcularOValorTotal()
    {
        var order = Order.Create(_orderTestFixture.RandomValidCpf(), new Cupom("VALE0", 0m));
        order.AddItem(new OrderItem("Produto 1", 100, 1));
        order.AddItem(new OrderItem("Produto 2", 100, 1));
        order.AddItem(new OrderItem("Produto 3", 100, 1));

        var total = order.CalculateTotal();

        total.Should().Be(300);
    }

    [Fact(DisplayName = nameof(NaoDeveSerPossivelCriarUmPedido_QuandoOCpfEInvalido))]
    [Trait("Domain", "Order")]
    public void NaoDeveSerPossivelCriarUmPedido_QuandoOCpfEInvalido()
    {
        var action = () =>
        {
            var cpf = Cpf.Create("000.000.00.00");
            return Order.Create(cpf, new Cupom("VALE0", 0m));
        };

        action.Should().Throw<ArgumentException>().WithMessage("O CPF deve ser válido");
    }

}
