namespace Domain.Entities;

public class OrderItem
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(string name, decimal price, int quantity)
    {
        Name = name;
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