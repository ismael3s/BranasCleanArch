using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Data.EF.Models;
[Table("product")]
public class Product
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;


    [Column("price")]
    public decimal Price { get; set; } = default;

    public List<OrderItem>? OrderItems { get; set; } = new();

    public Product()
    {

    }
}
