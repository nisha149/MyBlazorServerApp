using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlazorServerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStockQuantityToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkToPurchaseOrder",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "SupplierBillNo",
                table: "PurchaseInvoices");

            migrationBuilder.AlterColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkToPurchaseOrder",
                table: "PurchaseInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierBillNo",
                table: "PurchaseInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
