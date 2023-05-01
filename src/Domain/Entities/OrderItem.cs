namespace Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(Guid productId, decimal price, int quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Price = price;
        Quantity = quantity;

        Validate();
    }

    private void Validate()
    {
        if (Quantity <= 0) throw new ArgumentException("Quantidade não pode ser menor do que um");
        if (Price < 0) throw new ArgumentException("Preço não pode ser menor do que zero");
    }


}