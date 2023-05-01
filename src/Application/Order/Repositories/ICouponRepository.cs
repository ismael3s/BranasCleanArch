using Domain.Entities;

namespace Application.Order.Repositories;
public interface ICouponRepository
{
    public Task<Coupon> FindByCode(string cupomCode);
}
