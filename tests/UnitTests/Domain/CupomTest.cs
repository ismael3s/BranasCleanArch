using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain;
public class CupomTest
{

    [Fact(DisplayName = nameof(DeveSerPossivelCriarUmCupomQuandoOsDadosForemValidos))]
    [Trait("Domain", "Cupom")]
    public void DeveSerPossivelCriarUmCupomQuandoOsDadosForemValidos()
    {
        var cupom = Cupom.Create("VALE20", 20m);
        cupom.Should().NotBeNull();
        cupom.Should().BeOfType<Cupom>();
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmCupom_QuandoNaoHouverCodigo))]
    [Trait("Domain", "Cupom")]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null!)]
    public void NaoDeveSerPossivelCriarUmCupom_QuandoNaoHouverCodigo(string code)
    {
        var action = () => Cupom.Create(code, 20m);
        action.Should().Throw<ArgumentException>().WithMessage("O Código não pode ser vazio");
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMenorDoQueZero))]
    [Trait("Domain", "Cupom")]
    [InlineData(-0.3)]
    [InlineData(-100)]
    public void NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMenorDoQueZero(decimal discount)
    {
        var action = () => Cupom.Create("Cupom", discount);
        action.Should().Throw<ArgumentException>().WithMessage("O Desconto não pode ser menor do que zero");
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMaiorQue100))]
    [Trait("Domain", "Cupom")]
    [InlineData(101)]
    [InlineData(100.001)]
    public void NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMaiorQue100(decimal discount)
    {
        var action = () => Cupom.Create("Cupom", discount);
        action.Should().Throw<ArgumentException>().WithMessage("O Desconto não pode ser maior do que 100");
    }
}
