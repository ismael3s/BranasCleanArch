using Domain.VO;

namespace Domain.Entities;
public class Order
{
    public Guid Id { get; private set; }
    public Cpf Cpf { get; private set; }

    public Coupon? Cupom { get; private set; }

    public IList<OrderItem> Items { get; private set; }

    private Order(Cpf cpf, Coupon? cupom = null, Guid? id = default)
    {
        Id = id ?? Guid.NewGuid();
        Cpf = cpf;
        Items = new List<OrderItem>();
        Cupom = cupom;
    }

    public static Order Create(Cpf cpf, Coupon? cupom = null)
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

    public void ApplyCupom(Coupon cupom)
    {
        if (cupom is null) throw new ArgumentException("Não é possivel aplicar um cupom inexistente");
        Cupom = cupom;
    }
}
