using Application.Order.UseCases.Checkout;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Application.Order.UseCases;
[Collection(nameof(CheckoutFixture))]
public class CheckoutUseCaseTests
{
    private readonly CheckoutFixture _checkoutFixture;

    public CheckoutUseCaseTests(CheckoutFixture checkoutFixture)
    {
        _checkoutFixture = checkoutFixture;
    }

    [Fact(DisplayName = nameof(DeveCriarUmPedidoComTresItems))]
    [Trait("Application", "Order - UseCase")]
    public async Task DeveCriarUmPedidoComTresItems()
    {

        var cupomRepository = _checkoutFixture.GetCupomRepository();
        var orderRepository = _checkoutFixture.GetOrderRepository();
        var productRepository = _checkoutFixture.GetProductRepository();

        productRepository.Setup(r => r.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(
                _checkoutFixture.CreateProduct(100)
            );
        var unitOfWork = _checkoutFixture.GetUnitOfWork();
        var checkoutUseCase = new CheckoutUseCase(
            cupomRepository.Object,
            orderRepository.Object,
            unitOfWork.Object,
            productRepository.Object
        );
        var inputItems = new List<CheckoutInputItemDto>
        {
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1)
        };
        var input = new CheckoutInputDto("82897077034", inputItems);

        var result = await checkoutUseCase.Handle(input, CancellationToken.None);

        result.Total.Should().Be(300);
    }

    [Fact(DisplayName = nameof(NaoDeveSerPossivelCriarUmPedido_QuandoOCpfEInvalido))]
    [Trait("Application", "Order - UseCase")]
    public async Task NaoDeveSerPossivelCriarUmPedido_QuandoOCpfEInvalido()
    {
        var cupomRepository = _checkoutFixture.GetCupomRepository();
        var orderRepository = _checkoutFixture.GetOrderRepository();
        var unitOfWork = _checkoutFixture.GetUnitOfWork();
        var productRepository = _checkoutFixture.GetProductRepository();
        var checkoutUseCase = new CheckoutUseCase(
            cupomRepository.Object,
            orderRepository.Object,
            unitOfWork.Object,
            productRepository.Object
        );
        var inputItems = new List<CheckoutInputItemDto>
        {
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1)
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
        var cupomRepository = _checkoutFixture.GetCupomRepository();
        cupomRepository.Setup(mock =>
            mock.FindByCode(It.IsAny<string>())
        ).Returns<string>(parameters =>
        {
            var expiresAt = DateTime.Now.AddDays(1);
            Task<Coupon> task = parameters switch
            {
                "VALE20" => Task.FromResult(Coupon.Create(parameters, 20M, expiresAt: expiresAt)),
                "VALE10" => Task.FromResult(Coupon.Create(parameters, 10M, expiresAt: expiresAt)),
                _ => Task.FromResult<Coupon>(null!)
            };
            return task;
        });
        var orderRepository = _checkoutFixture.GetOrderRepository();
        var productRepository = _checkoutFixture.GetProductRepository();
        productRepository.Setup(r => r.GetById(It.IsAny<Guid>()))
              .ReturnsAsync(
                  _checkoutFixture.CreateProduct(100)
              );
        var unitOfWork = _checkoutFixture.GetUnitOfWork();
        var checkoutUseCase = new CheckoutUseCase(
            cupomRepository.Object,
            orderRepository.Object,
            unitOfWork.Object,
            productRepository.Object
        );
        var inputItems = new List<CheckoutInputItemDto>
        {
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1)
        };
        var input = new CheckoutInputDto("040.635.420-09", inputItems, cupomCode);
        var result = await checkoutUseCase.Handle(input, CancellationToken.None);

        result.Total.Should().Be(expectedTotal);
    }

    [Fact(DisplayName = nameof(NaoDeveAplicarUmCupomDeDescontoExpiradoNoPedido))]
    [Trait("Application", "Order - UseCase")]
    public async Task NaoDeveAplicarUmCupomDeDescontoExpiradoNoPedido()
    {
        var cupomRepository = _checkoutFixture.GetCupomRepository();
        cupomRepository.Setup(mock =>
            mock.FindByCode(It.IsAny<string>())
        ).Returns(
            Task.FromResult(Coupon.Create("VALLE20", 20M, expiresAt: DateTime.Now.AddSeconds(-30)))
        );
        var orderRepository = _checkoutFixture.GetOrderRepository();
        var productRepository = _checkoutFixture.GetProductRepository();
        productRepository.Setup(r => r.GetById(It.IsAny<Guid>()))
              .ReturnsAsync(
                  _checkoutFixture.CreateProduct(100)
              );
        var unitOfWork = _checkoutFixture.GetUnitOfWork();
        var checkoutUseCase = new CheckoutUseCase(
            cupomRepository.Object,
            orderRepository.Object,
            unitOfWork.Object,
            productRepository.Object
        );
        var inputItems = new List<CheckoutInputItemDto>
        {
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1),
           _checkoutFixture.CreateValidCheckoutInputItemDto(1)
        };
        var input = new CheckoutInputDto("040.635.420-09", inputItems, "VALE20");
        var result = async () => await checkoutUseCase.Handle(input, CancellationToken.None);
        await result.Should().ThrowAsync<Exception>();

    }
}
