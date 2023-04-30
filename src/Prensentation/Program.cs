using Application;
using Infra.IoC;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/checkout", (ISomeService someService) =>
{
    someService.Do();
    return true;
})
.WithName("Checkout")
.WithTags("Checkout");

app.Run();

public partial class Program { }

