using Application.Order.UseCases.Checkout;
using FluentAssertions;
using Infra.Data.EF;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IntegrationTests.RestAPI.Checkout;
[Collection(nameof(SharedTestCollection))]
public class CheckoutControllerTests : IAsyncLifetime
{
    private readonly ApplicationWebFactory _factory;
    private readonly AppDbContext _dbContext = default!;
    public CheckoutControllerTests(ApplicationWebFactory factory)
    {
        _factory = factory;
        //_dbContext = factory.Services.GetRequiredService<AppDbContext>();
    }
    [Fact]
    public async Task CheckoutController_Checkout_ShouldReturnOk()
    {
        // Arrange
        var client = _factory.HttpClient;

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

        //var orders = _dbContext.Orders.ToListAsync();

        checkoutOutput.Should().NotBeNull();
        checkoutOutput!.Total.Should().Be(400M);
        checkoutOutput!.Id.Should().NotBeEmpty();

    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }
}
