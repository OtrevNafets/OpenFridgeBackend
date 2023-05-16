using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.OpenFridge.Migrations
{
    /// <inheritdoc />
    public partial class RelationIngridient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingridients_Recipes_RecipeId",
                table: "Ingridients");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Ingridients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingridients_Recipes_RecipeId",
                table: "Ingridients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingridients_Recipes_RecipeId",
                table: "Ingridients");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Ingridients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingridients_Recipes_RecipeId",
                table: "Ingridients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }
    }
}
