using Domain.Exceptions;

namespace Domain.Entities;
public class Product
{

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    private Product(string name, decimal price, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Price = price;
        Validate();
    }

    public static Product Create(string name, decimal price, Guid? id = null)
    {
        return new Product(name, price, id);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new DomainException("O Nome do produto não pode ser vazio");
        if (Price <= 0) throw new DomainException("O Preço do produto deve ser maior que zero");
    }
}
