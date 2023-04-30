using Domain.VO;

namespace Domain.Entities;
public class Order
{
    public Cpf Cpf { get; private set; }

    public Cupom? Cupom { get; private set; }

    public IList<OrderItem> Items { get; private set; }

    private Order(Cpf cpf, Cupom? cupom = null)
    {
        Cpf = cpf;
        Items = new List<OrderItem>();
        Cupom = cupom;
    }

    public static Order Create(Cpf cpf, Cupom? cupom = null)
    {
        var order = new Order(cpf, cupom);
        return order;
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public decimal CalculateTotal()
    {
        var sumOfItems = Items.Sum(x => x.Price * x.Quantity);
        if (Cupom is not null)
        {
            sumOfItems *= 1 - Cupom.Discount / 100;
        }
        return sumOfItems;
    }
}
