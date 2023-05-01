namespace Domain.Entities;
public class Coupon
{

    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public decimal Discount { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public Coupon(Guid? id, string code, decimal discount, DateTime expiresAt)
    {
        Id = id ?? Guid.NewGuid();
        Code = code;
        Discount = discount;
        ExpiresAt = expiresAt;
        Validate();
    }

    public static Coupon Create(string code, decimal discount, Guid? id = default, DateTime expiresAt = default!)
    {
        return new Coupon(id, code, discount, expiresAt);
    }
    public bool IsExpired(DateTime currentDate) => ExpiresAt < currentDate;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Code)) throw new ArgumentException("O Código não pode ser vazio");
        if (Discount < 0) throw new ArgumentException("O Desconto não pode ser menor do que zero");
        if (Discount > 100) throw new ArgumentException("O Desconto não pode ser maior do que 100");
    }
}
