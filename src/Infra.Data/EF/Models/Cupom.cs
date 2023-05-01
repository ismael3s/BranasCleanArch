using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Data.EF.Models;
[Table("cupom")]
public class Cupom
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("code")]
    public string Code { get; set; } = string.Empty;
    [Column("discount")]
    public decimal Discount { get; set; }

    public Order? Order { get; set; }

}
