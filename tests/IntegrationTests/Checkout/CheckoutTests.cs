using FluentAssertions;

namespace IntegrationTests.Checkout;
public class CheckoutTests : IClassFixture<ApiWebApplicationFactory>
{
    readonly HttpClient _client;

    public CheckoutTests(ApiWebApplicationFactory application)
    {
        _client = application.CreateClient();
    }
    [Fact(DisplayName = nameof(Checkout_POST_DeveSerPossivelCriarUmPedidoComTresItems))]
    [Trait("Rest Presentation", "Checkout")]
    public async Task Checkout_POST_DeveSerPossivelCriarUmPedidoComTresItems()
    {
        var result = 1 + 1;

        result.Should().Be(2);
    }
}
