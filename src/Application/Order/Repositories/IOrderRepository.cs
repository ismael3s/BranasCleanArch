using Entities = Domain.Entities;
namespace Application.Order.Repositories;
public interface IOrderRepository
{
    Task Add(Entities.Order order);
}
