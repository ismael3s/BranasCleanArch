using Application.Order.Repositories;
using Application.Order.UseCases.Checkout;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Application.Order.UseCases;
public class CheckoutUseCaseTests
{
    [Fact(DisplayName = nameof(DeveCriarUmPedidoComTresItems))]
    [Trait("Application", "Order - UseCase")]
    public async Task DeveCriarUmPedidoComTresItems()
    {
        var cupomRepository = new Mock<ICupomRepository>();
        var orderRepository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var checkoutUseCase = new CheckoutUseCase(cupomRepository.Object, orderRepository.Object, unitOfWork.Object);
        var inputItems = new List<CheckoutInputItemDto>
        {
            new CheckoutInputItemDto("Produto 1", 100, 1),
            new CheckoutInputItemDto("Produto 2", 100, 1),
            new CheckoutInputItemDto("Produto 3", 100, 1)
        };
        var input = new CheckoutInputDto("82897077034", inputItems);

        var result = await checkoutUseCase.Handle(input, CancellationToken.None);

        result.Total.Should().Be(300);
    }

    [Fact(DisplayName = nameof(NaoDeveSerPossivelCriarUmPedido_QuandoOCpfEInvalido))]
    [Trait("Application", "Order - UseCase")]
    public async Task NaoDeveSerPossivelCriarUmPedido_QuandoOCpfEInvalido()
    {
        var cupomRepository = new Mock<ICupomRepository>();
        var orderRepository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var checkoutUseCase = new CheckoutUseCase(cupomRepository.Object, orderRepository.Object, unitOfWork.Object);
        var inputItems = new List<CheckoutInputItemDto>
        {
            new CheckoutInputItemDto("Produto 1", 100, 1),
            new CheckoutInputItemDto("Produto 2", 100, 1),
            new CheckoutInputItemDto("Produto 3", 100, 1)
        };
        var input = new CheckoutInputDto("000.000.00-00", inputItems);

        var action = async () => await checkoutUseCase.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<ArgumentException>().WithMessage("O CPF deve ser válido");
    }

    [Theory(DisplayName = nameof(DeveCriarUmPedidoComTresItemsAplicandoOCupomDeDescontoNoValorTotal))]
    [Trait("Application", "Order - UseCase")]
    [MemberData(nameof(CheckoutTestMemberData.GetCuponsForTest), MemberType = typeof(CheckoutTestMemberData))]
    public async Task DeveCriarUmPedidoComTresItemsAplicandoOCupomDeDescontoNoValorTotal(string cupomCode, decimal expectedTotal)
    {
        var cupomRepository = new Mock<ICupomRepository>();
        cupomRepository.Setup(mock =>
            mock.FindByCode(It.IsAny<string>())
        ).Returns<string>(parameters =>
        {
            Task<Cupom> task = parameters switch
            {
                "VALE20" => Task.FromResult(Cupom.Create(parameters, 20M)),
                "VALE10" => Task.FromResult(Cupom.Create(parameters, 10M)),
                _ => Task.FromResult<Cupom>(null!)
            };
            return task;
        });
        var orderRepository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var checkoutUseCase = new CheckoutUseCase(cupomRepository.Object, orderRepository.Object, unitOfWork.Object);
        var inputItems = new List<CheckoutInputItemDto>
        {
            new CheckoutInputItemDto("Produto 1", 100, 1),
            new CheckoutInputItemDto("Produto 2", 100, 1),
            new CheckoutInputItemDto("Produto 3", 100, 1)
        };
        var input = new CheckoutInputDto("040.635.420-09", inputItems, cupomCode);
        var result = await checkoutUseCase.Handle(input, CancellationToken.None);

        result.Total.Should().Be(expectedTotal);
    }
}
