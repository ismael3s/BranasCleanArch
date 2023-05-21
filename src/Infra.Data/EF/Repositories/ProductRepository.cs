using Application.Order.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EF.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetById(Guid id)
    {
        var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Produto não encontrado");
        return Product.Create(productModel.Name, productModel.Price, productModel.Id);
    }
}
