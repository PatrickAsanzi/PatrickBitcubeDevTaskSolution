using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addcheckoutitemproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CheckoutItems_CheckoutItemId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CheckoutItemId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CheckoutItemId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "CheckoutItemProduct",
                columns: table => new
                {
                    CheckoutItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutItemProduct", x => new { x.CheckoutItemId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CheckoutItemProduct_CheckoutItems_CheckoutItemId",
                        column: x => x.CheckoutItemId,
                        principalTable: "CheckoutItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutItemProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutItemProduct_ProductId",
                table: "CheckoutItemProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutItemProduct");

            migrationBuilder.AddColumn<int>(
                name: "CheckoutItemId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CheckoutItemId",
                table: "Products",
                column: "CheckoutItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CheckoutItems_CheckoutItemId",
                table: "Products",
                column: "CheckoutItemId",
                principalTable: "CheckoutItems",
                principalColumn: "Id");
        }
    }
}
