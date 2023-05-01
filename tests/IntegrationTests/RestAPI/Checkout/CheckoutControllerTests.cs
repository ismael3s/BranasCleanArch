using Application.Order.UseCases.Checkout;
using FluentAssertions;
using Infra.Data.EF;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace IntegrationTests.RestAPI.Checkout;
[Collection(nameof(SharedTestCollection))]
public class CheckoutControllerTests : ControllerTestBase
{
    private readonly ApplicationWebFactory _applicationWebFactory;
    public CheckoutControllerTests(ApplicationWebFactory factory) : base(factory)
    {
        _applicationWebFactory = factory;
    }
    [Fact(DisplayName = nameof(POST_CheckoutController_DeveSerPossivelCriarUmPedidoCom3Produtos))]
    [Trait("Integration/CheckoutController", "Checkout - POST")]
    public async Task POST_CheckoutController_DeveSerPossivelCriarUmPedidoCom3Produtos()
    {
        Func<AppDbContext, Task> actions = async (AppDbContext db) =>
        {
            db.Database.EnsureCreated();
            db?.Products?.Add(new Infra.Data.EF.Models.Product()
            {
                Id = Guid.Parse("493bc090-7961-432c-8794-c2a2407ca321"),
                Name = "Produto 1",
                Price = 200M,
            });
            await db!.SaveChangesAsync();
        };

        await _applicationWebFactory.SeedWithAsync(actions);

        var items = new List<CheckoutInputItemDto>()
        {
            new CheckoutInputItemDto("493bc090-7961-432c-8794-c2a2407ca321", 2)
        };
        var checkoutInput = new CheckoutInputDto("63966871009", items);

        var response = await this.Client.PostAsJsonAsync("/checkout", checkoutInput);

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

        var items = new List<CheckoutInputItemDto>()
        {
            new CheckoutInputItemDto("Produto 1", 2)
        };
        var checkoutInput = new CheckoutInputDto("000.000.000-00", items);
        // Act
        var response = await this.Client.PostAsJsonAsync("/checkout", checkoutInput);

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

