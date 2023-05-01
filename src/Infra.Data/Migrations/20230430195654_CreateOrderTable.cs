using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class CreateOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    cupom_id = table.Column<Guid>(type: "uuid", nullable: true),
                    total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_cupom_cupom_id",
                        column: x => x.cupom_id,
                        principalTable: "cupom",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_item_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_cupom_id",
                table: "order",
                column: "cupom_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_item_order_id",
                table: "order_item",
                column: "order_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "order");
        }
    }
}
