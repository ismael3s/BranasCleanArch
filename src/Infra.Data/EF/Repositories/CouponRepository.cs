using Application.Order.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EF.Repositories;
public sealed class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;
    public CouponRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Coupon> FindByCode(string cupomCode)
    {
        var cupomModel = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == cupomCode) ?? throw new ArgumentException("Cupom não encontrado");

        return new Coupon(cupomModel.Id, cupomModel.Code, cupomModel.Discount);
    }
}
