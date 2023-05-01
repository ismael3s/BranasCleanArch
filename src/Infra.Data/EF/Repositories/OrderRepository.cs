using Application.Order.Repositories;
using Domain.Entities;

namespace Infra.Data.EF.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Order order)
    {
        var orderModel = new Models.Order()
        {
            Id = order.Id,
            Cpf = order.Cpf.Value,
            CupomId = order.Cupom?.Id,
            Total = order.CalculateTotal(),
        };

        var orderItems = order.Items.Select(orderItem => new Models.OrderItem()
        {
            Id = orderItem.Id,
            OrderId = order.Id,
            Price = orderItem.Price,
            ProductName = orderItem.Name,
            Quantity = orderItem.Quantity
        }).ToList();

        orderModel.OrderItems = orderItems;
        await _context.Orders.AddAsync(orderModel);
    }
}
