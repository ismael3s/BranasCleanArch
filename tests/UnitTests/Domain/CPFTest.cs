using Domain.VO;
using FluentAssertions;

namespace UnitTests.Domain;
public class CPFTest
{
    [Theory(DisplayName = nameof(DeveSerPossivelCriarUmCPFQuandoOsDadosForemValidos))]
    [Trait("Domain", "CPF")]
    [InlineData("00861120078")]
    [InlineData("12270553071")]
    [InlineData("82615204041")]
    [InlineData("146.515.600-32")]
    public void DeveSerPossivelCriarUmCPFQuandoOsDadosForemValidos(string value)
    {
        var cpf = Cpf.Create(value);
        var valueWithoutMask = value.Replace(".", "").Replace("-", "");

        cpf.Should().NotBeNull();
        cpf.Should().BeOfType<Cpf>();
        cpf.Value.Should().Be(valueWithoutMask);
    }

    [Theory(DisplayName = nameof(NãoDeveSerPossivelCriarUmCpf_QuandoOValorPassadoEInvalido))]
    [Trait("Domain", "CPF")]
    [InlineData("00000000000")]
    [InlineData("12345678900")]
    [InlineData("123")]
    [InlineData(null!)]
    public void NãoDeveSerPossivelCriarUmCpf_QuandoOValorPassadoEInvalido(string value)
    {
        var action = () => Cpf.Create(value);

        action.Should().Throw<ArgumentException>().WithMessage("O CPF deve ser válido");
    }

}
