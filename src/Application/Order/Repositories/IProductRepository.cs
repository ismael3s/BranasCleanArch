using Domain.Entities;

namespace Application.Order.Repositories;
public interface IProductRepository
{
    Task<Product> GetById(Guid id);
}
