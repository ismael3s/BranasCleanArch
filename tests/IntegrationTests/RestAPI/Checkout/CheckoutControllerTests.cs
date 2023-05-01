using Application.Order.UseCases.Checkout;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace IntegrationTests.RestAPI.Checkout;
[Collection(nameof(SharedTestCollection))]
public class CheckoutControllerTests
{
    private readonly ApplicationWebFactory _factory;
    public CheckoutControllerTests(ApplicationWebFactory factory)
    {
        _factory = factory;
    }
    [Fact(DisplayName = nameof(POST_CheckoutController_DeveSerPossivelCriarUmPedidoCom3Produtos))]
    [Trait("Integration/CheckoutController", "Checkout - POST")]
    public async Task POST_CheckoutController_DeveSerPossivelCriarUmPedidoCom3Produtos()
    {
        // Arrange
        var client = _factory.CreateClient();
        var items = new List<CheckoutInputItemDto>()
        {
            new CheckoutInputItemDto("Produto 1", 200M, 2)
        };
        var checkoutInput = new CheckoutInputDto("63966871009", items);
        // Act
        var response = await client.PostAsJsonAsync("/checkout", checkoutInput);

        // Assert
        response.EnsureSuccessStatusCode();
        var checkoutOutput = JsonConvert.DeserializeObject<CheckoutOutputDto>(
            await response.Content.ReadAsStringAsync()
        );
        checkoutOutput.Should().NotBeNull();
        checkoutOutput!.Total.Should().Be(400M);
        checkoutOutput!.Id.Should().NotBeEmpty();
    }

    [Fact(DisplayName = nameof(POST_CheckoutController_NaoDeveSerPossivelCriarUmPedidoComCpfInvalido))]
    [Trait("Integration/CheckoutController", "Checkout - POST")]
    public async Task POST_CheckoutController_NaoDeveSerPossivelCriarUmPedidoComCpfInvalido()
    {
        // Arrange
        var client = _factory.CreateClient();
        var items = new List<CheckoutInputItemDto>()
        {
            new CheckoutInputItemDto("Produto 1", 200M, 2)
        };
        var checkoutInput = new CheckoutInputDto("000.000.000-00", items);
        // Act
        var response = await client.PostAsJsonAsync("/checkout", checkoutInput);

        // Assert
        var checkoutOutput = JsonConvert.DeserializeObject<Error>(
            await response.Content.ReadAsStringAsync()
        );

        checkoutInput.Should().NotBeNull();
        checkoutOutput!.Message.Should().Be("O CPF deve ser válido");
        checkoutOutput!.Status.Should().Be(400);
    }
}

public record Error(string Message, int Status);

