﻿namespace Domain.Entities;
public class Cupom
{

    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public decimal Discount { get; private set; }
    public Cupom(Guid? id, string code, decimal discount)
    {
        Id = id ?? Guid.NewGuid();
        Code = code;
        Discount = discount;

        Validate();
    }

    public static Cupom Create(string code, decimal discount, Guid? id = default)
    {
        return new Cupom(id, code, discount);
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Code)) throw new ArgumentException("O Código não pode ser vazio");
        if (Discount < 0) throw new ArgumentException("O Desconto não pode ser menor do que zero");
        if (Discount > 100) throw new ArgumentException("O Desconto não pode ser maior do que 100");
    }
}
