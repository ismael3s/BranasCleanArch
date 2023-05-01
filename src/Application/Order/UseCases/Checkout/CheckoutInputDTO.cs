namespace Application.Order.UseCases.Checkout;
public record CheckoutInputDto(string Cpf, List<CheckoutInputItemDto> Items, string? CouponCode = null)
{
}


public record CheckoutInputItemDto(string ProductId, int Quantity)
{
}