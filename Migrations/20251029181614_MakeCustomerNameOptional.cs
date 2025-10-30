using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlazorServerApp.Migrations
{
    /// <inheritdoc />
    public partial class MakeCustomerNameOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_SupplierId",
                table: "PurchaseInvoices",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Suppliers_SupplierId",
                table: "PurchaseInvoices",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Suppliers_SupplierId",
                table: "PurchaseInvoices");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseInvoices_SupplierId",
                table: "PurchaseInvoices");
        }
    }
}
