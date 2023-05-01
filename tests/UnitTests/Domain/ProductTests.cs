using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;

namespace UnitTests.Domain;
public class ProductTests
{
    [Fact(DisplayName = nameof(DeveCriarUmProdutoValido))]
    [Trait("Domain", "Product")]
    public void DeveCriarUmProdutoValido()
    {
        var product = Product.Create("Produto 1", 100);


        product.Should().NotBeNull();
        product.Id.Should().NotBeEmpty();
        product.Name.Should().Be("Produto 1");
        product.Price.Should().Be(100);
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmProdutoComUmNomeInvalido))]
    [Trait("Domain", "Product")]
    [InlineData("   ")]
    [InlineData("")]
    [InlineData(null!)]
    public void NaoDeveSerPossivelCriarUmProdutoComUmNomeInvalido(string? name)
    {
        var action = () => Product.Create(name!, 100);
        action.Should().Throw<DomainException>().WithMessage("O Nome do produto não pode ser vazio");
    }

    [Theory(DisplayName = nameof(NaoDeveSerPossivelCriarUmProdutoComPrecoMenorOuIgualAZero))]
    [Trait("Domain", "Product")]
    [InlineData(0)]
    [InlineData(-123)]
    public void NaoDeveSerPossivelCriarUmProdutoComPrecoMenorOuIgualAZero(decimal price)
    {
        var action = () => Product.Create("Produto 1", price);
        action.Should().Throw<DomainException>().WithMessage("O Preço do produto deve ser maior que zero");
    }
}
