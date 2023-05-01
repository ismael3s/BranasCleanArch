using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Data.EF.Models;
[Table("order_item")]
public class OrderItem
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("product_name")]
    public string ProductName { get; set; } = string.Empty;

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("order_id")]
    public Guid OrderId { get; set; }

    public Order Order { get; set; } = null!;
}
