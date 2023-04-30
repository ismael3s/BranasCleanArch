using Application.Order.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EF.Repositories;
public sealed class CupomRepository : ICupomRepository
{
    private readonly AppDbContext _context;
    public CupomRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Cupom> FindByCode(string cupomCode)
    {
        var cupomModel = await _context.Cupoms.FirstOrDefaultAsync(c => c.Code == cupomCode) ?? throw new ArgumentException("Cupom não encontrado");

        return new Cupom(cupomModel.Code, cupomModel.Discount);
    }
}
