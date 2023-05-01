using Application.Order.Repositories;
using Domain.Entities;
using Domain.VO;
using Entities = Domain.Entities;

namespace Application.Order.UseCases.Checkout;
public class CheckoutUseCase : ICheckoutUseCase
{

    private readonly ICouponRepository _cupomRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutUseCase(
        ICouponRepository cupomRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IProductRepository productRepository)
    {
        _cupomRepository = cupomRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CheckoutOutputDto> Handle(CheckoutInputDto input, CancellationToken cancellationToken)
    {
        var cpf = Cpf.Create(input.Cpf);
        var order = Entities.Order.Create(cpf);

        if (input.CouponCode is not null)
        {
            var cupom = await _cupomRepository.FindByCode(input.CouponCode);
            order.ApplyCupom(cupom);
        }

        input.Items.ForEach(async item =>
        {
            var product = await _productRepository.GetById(Guid.Parse(item.ProductId));
            var orderItem = new OrderItem(product.Id, product.Price, item.Quantity);
            order.AddItem(orderItem);
        });

        await _orderRepository.Add(order);
        await _unitOfWork.CommitAsync();

        return new CheckoutOutputDto(order.Id, order.CalculateTotal());
    }
}
