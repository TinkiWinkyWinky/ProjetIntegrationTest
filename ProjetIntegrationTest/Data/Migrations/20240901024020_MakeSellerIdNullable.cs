using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetIntegrationTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeSellerIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Sellers_SellerId",
                table: "Lots");

            migrationBuilder.AlterColumn<int>(
                name: "SellerId",
                table: "Lots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Sellers_SellerId",
                table: "Lots",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Sellers_SellerId",
                table: "Lots");

            migrationBuilder.AlterColumn<int>(
                name: "SellerId",
                table: "Lots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Sellers_SellerId",
                table: "Lots",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
