using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Data.EF.Models;
[Table("order")]
public class Order
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("cpf")]
    public string Cpf { get; set; } = string.Empty;

    public Cupom? Cupom { get; set; }

    [Column("cupom_id")]
    public Guid? CupomId { get; set; }

    [Column("total")]
    public decimal Total { get; set; }

    public List<OrderItem> OrderItems = new();
}
