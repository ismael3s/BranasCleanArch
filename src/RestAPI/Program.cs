using Application.Order.UseCases.Checkout;
using Infra.IoC;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfra(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/checkout", async (CheckoutInputDto checkoutInputDto, ICheckoutUseCase useCase) =>
{
    var output = await useCase.Handle(checkoutInputDto, CancellationToken.None);
    return output;
})
.WithName("Checkout");

app.Run();

