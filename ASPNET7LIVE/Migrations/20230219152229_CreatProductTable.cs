using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET7LIVE.Migrations
{
    public partial class CreatProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productc_Category_CategoryId",
                table: "Productc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productc",
                table: "Productc");

            migrationBuilder.RenameTable(
                name: "Productc",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Productc_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Productc");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Productc",
                newName: "IX_Productc_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productc",
                table: "Productc",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productc_Category_CategoryId",
                table: "Productc",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
