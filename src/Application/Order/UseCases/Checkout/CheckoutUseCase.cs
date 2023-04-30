using Application.Order.Repositories;
using Application.SeedWork;
using Domain.Entities;
using Domain.VO;
using Entities = Domain.Entities;

namespace Application.Order.UseCases.Checkout;
public class CheckoutUseCase : IUseCase<CheckoutInputDto, CheckoutOutputDto>
{

    private readonly ICupomRepository _cupomRepository;

    public CheckoutUseCase(ICupomRepository cupomRepository)
    {
        _cupomRepository = cupomRepository;
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

        return new CheckoutOutputDto(order.Id, order.CalculateTotal());
    }
}
