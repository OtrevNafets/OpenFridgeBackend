using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.OpenFridge.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Recipes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IngridientId",
                table: "Ingridients",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeName",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Recipes",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ingridients",
                newName: "IngridientId");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeName",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
