﻿using Application.Order.UseCases.Checkout;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IntegrationTests.RestAPI.Checkout;
public class CheckoutControllerTests : IClassFixture<ApplicationWebFactory>
{
    private readonly ApplicationWebFactory _factory;
    public CheckoutControllerTests(ApplicationWebFactory factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task CheckoutController_Checkout_ShouldReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        var items = new List<CheckoutInputItemDto>()
        {
            new CheckoutInputItemDto("Produto 1", 200M, 2)
        };
        var checkoutInput = new CheckoutInputDto("07242927560", items);
        // Act
        var response = await client.PostAsJsonAsync("/checkout", checkoutInput);
        // Assert
        response.EnsureSuccessStatusCode();
        var checkoutOutput = JsonConvert.DeserializeObject<CheckoutOutputDto>(
            await response.Content.ReadAsStringAsync()
        );

        checkoutOutput.Should().NotBeNull();
        checkoutOutput!.Total.Should().Be(401M);
        checkoutOutput!.Id.Should().NotBeEmpty();
    }
}