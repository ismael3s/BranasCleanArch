using Domain.Entities;

namespace Application.Order.Repositories;
public interface ICupomRepository
{
    public Task<Cupom> FindByCode(string cupomCode);
}
