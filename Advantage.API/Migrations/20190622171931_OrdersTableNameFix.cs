using Microsoft.EntityFrameworkCore.Migrations;

namespace Advantage.API.Migrations
{
    public partial class OrdersTableNameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ORders_Customers_CustomerID",
                table: "ORders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ORders",
                table: "ORders");

            migrationBuilder.RenameTable(
                name: "ORders",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_ORders_CustomerID",
                table: "Orders",
                newName: "IX_Orders_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "ORders");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerID",
                table: "ORders",
                newName: "IX_ORders_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ORders",
                table: "ORders",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ORders_Customers_CustomerID",
                table: "ORders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
