using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class RenameCupomToCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_cupom_cupom_id",
                table: "order");

            migrationBuilder.DropTable(
                name: "cupom");

            migrationBuilder.CreateTable(
                name: "coupon",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    discount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coupon", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_order_coupon_cupom_id",
                table: "order",
                column: "cupom_id",
                principalTable: "coupon",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_coupon_cupom_id",
                table: "order");

            migrationBuilder.DropTable(
                name: "coupon");

            migrationBuilder.CreateTable(
                name: "cupom",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    discount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cupom", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_order_cupom_cupom_id",
                table: "order",
                column: "cupom_id",
                principalTable: "cupom",
                principalColumn: "id");
        }
    }
}
