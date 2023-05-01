using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain;
public class CouponTests
{

    [Fact(DisplayName = nameof(DeveSerPossivelCriarUmCupomQuandoOsDadosForemValidos))]
    [Trait("Domain", "Cupom")]
    public void DeveSerPossivelCriarUmCupomQuandoOsDadosForemValidos()
    {
        var cupom = Coupon.Create("VALE20", 20m);
        cupom.Should().NotBeNull();
        cupom.Should().BeOfType<Coupon>();
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmCupom_QuandoNaoHouverCodigo))]
    [Trait("Domain", "Cupom")]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null!)]
    public void NaoDeveSerPossivelCriarUmCupom_QuandoNaoHouverCodigo(string code)
    {
        var action = () => Coupon.Create(code, 20m);
        action.Should().Throw<ArgumentException>().WithMessage("O Código não pode ser vazio");
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMenorDoQueZero))]
    [Trait("Domain", "Cupom")]
    [InlineData(-0.3)]
    [InlineData(-100)]
    public void NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMenorDoQueZero(decimal discount)
    {
        var action = () => Coupon.Create("Cupom", discount);
        action.Should().Throw<ArgumentException>().WithMessage("O Desconto não pode ser menor do que zero");
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMaiorQue100))]
    [Trait("Domain", "Cupom")]
    [InlineData(101)]
    [InlineData(100.001)]
    public void NaoDeveSerPossivelCriarUmCupom_QuandoODescontoForMaiorQue100(decimal discount)
    {
        var action = () => Coupon.Create("Cupom", discount);
        action.Should().Throw<ArgumentException>().WithMessage("O Desconto não pode ser maior do que 100");
    }

    [Fact(DisplayName = nameof(AoPassarUmaDataMaiorDoQueADataDeExpiracaoDeveRetornarTrue))]
    [Trait("Domain", "Coupon")]
    public void AoPassarUmaDataMaiorDoQueADataDeExpiracaoDeveRetornarTrue()
    {
        var expiresAt = DateTime.Now;
        var currentDate = DateTime.Now.AddSeconds(60);

        var coupon = Coupon.Create("VALE20", 10, null, expiresAt);

        coupon.IsExpired(currentDate).Should().BeTrue();
    }

    [Fact(DisplayName = nameof(AoPassarUmaDataMenorDoQueADataDeExpiracaoDeveRetornarFalse))]
    [Trait("Domain", "Coupon")]
    public void AoPassarUmaDataMenorDoQueADataDeExpiracaoDeveRetornarFalse()
    {
        var expiresAt = DateTime.Now.AddSeconds(10);
        var currentDate = DateTime.Now;

        var coupon = Coupon.Create("VALE20", 10, null, expiresAt);

        coupon.IsExpired(currentDate).Should().BeFalse();
    }
}
