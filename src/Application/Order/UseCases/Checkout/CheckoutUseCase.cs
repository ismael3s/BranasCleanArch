using Application.Order.Repositories;
using Domain.Entities;
using Domain.VO;
using Entities = Domain.Entities;

namespace Application.Order.UseCases.Checkout;
public class CheckoutUseCase : ICheckoutUseCase
{

    private readonly ICupomRepository _cupomRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutUseCase(ICupomRepository cupomRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _cupomRepository = cupomRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CheckoutOutputDto> Handle(CheckoutInputDto input, CancellationToken cancellationToken)
    {
        var cpf = Cpf.Create(input.Cpf);
        var order = Entities.Order.Create(cpf);

        if (input.CupomCode is not null)
        {
            var cupom = await _cupomRepository.FindByCode(input.CupomCode);
            order.ApplyCupom(cupom);
        }

        input.Items.ForEach(item =>
        {
            var orderItem = new OrderItem(item.Description, item.Price, item.Quantity);
            order.AddItem(orderItem);
        });

        await _orderRepository.Add(order);
        await _unitOfWork.CommitAsync();

        return new CheckoutOutputDto(order.Id, order.CalculateTotal());
    }
}
