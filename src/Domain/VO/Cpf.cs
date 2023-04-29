using System.Text.RegularExpressions;

namespace Domain.VO;
public record Cpf
{
    public string Value { get; private set; }
    private Cpf(string value)
    {
        Value = SanatizeValue(value);
        Validate();
    }

    private static string SanatizeValue(string value) => Regex.Replace(value ?? "", @"\D", "")
        .Trim();


    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value)) throw new ArgumentException("O CPF deve ser válido");
        if (Value.Length is < 11 or > 14) throw new ArgumentException("O CPF deve ser válido");

        if (!Valida(Value)) throw new ArgumentException("O CPF deve ser válido");

    }
    private bool IsAllSameDigit() => Value.Distinct().Count() == 1;


    private static string CalculateDigits(string value, int[] multiplier, int loopCount = 9)
    {
        var sum = 0;
        for (int i = 0; i < loopCount; i++)
            sum += int.Parse(value[i].ToString()) * multiplier[i];
        var rest = sum % 11;
        var digit = rest < 2 ? 0 : 11 - rest;
        return digit.ToString();
    }
    private bool Valida(string cpf)

    {
        if (IsAllSameDigit()) return false;
        int[] multiplierForFirstDigit = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplierForSecondDigit = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var firstNineNumberFromCPF = cpf[..9];
        var digit = CalculateDigits(firstNineNumberFromCPF, multiplierForFirstDigit, 9);
        digit += CalculateDigits(firstNineNumberFromCPF + digit, multiplierForSecondDigit, 10);
        return cpf.EndsWith(digit);
    }

    public static Cpf Create(string value)
    {
        return new Cpf(value);
    }


}

