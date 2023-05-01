using Application.Order.UseCases.Checkout;
using Infra.IoC;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfra(configuration);
var app = builder.Build();

app.UseExceptionHandler(exceptionHandler => exceptionHandler.Run(
        async context =>
        {
            context.Response.ContentType = "application/json";
            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandler is not null)
            {
                var error = exceptionHandler.Error;

                var status = error switch
                {
                    ArgumentException => 400,
                    _ => 500
                };

                await context.Response.WriteAsJsonAsync(new
                {
                    status,
                    message = error.Message
                });
            }


        }
    ));
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



public partial class Program
{
    protected Program()
    {
    }
}