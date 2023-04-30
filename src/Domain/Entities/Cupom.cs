namespace Domain.Entities;
public class Cupom
{
    public string Code { get; private set; }
    public decimal Discount { get; private set; }
    public Cupom(string code, decimal discount)
    {
        Code = code;
        Discount = discount;

        Validate();
    }

    public static Cupom Create(string code, decimal discount)
    {
        return new Cupom(code, discount);
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Code)) throw new ArgumentException("O Código não pode ser vazio");
        if (Discount < 0) throw new ArgumentException("O Desconto não pode ser menor do que zero");
        if (Discount > 100) throw new ArgumentException("O Desconto não pode ser maior do que 100");
    }
}
