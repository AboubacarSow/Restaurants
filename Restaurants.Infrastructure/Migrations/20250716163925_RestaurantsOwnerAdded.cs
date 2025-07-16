using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantsOwnerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId1",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_OwnerId1",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_OwnerId1",
                table: "Restaurants",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId1",
                table: "Restaurants",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
