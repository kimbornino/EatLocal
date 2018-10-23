using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EatLocal.Data.Migrations
{
    public partial class addedjunctiontoDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalFoodRecipe",
                columns: table => new
                {
                    LocalFoodRecipeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocalFoodID = table.Column<int>(nullable: false),
                    RecipeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalFoodRecipe", x => x.LocalFoodRecipeID);
                    table.ForeignKey(
                        name: "FK_LocalFoodRecipe_LocalFood_LocalFoodID",
                        column: x => x.LocalFoodID,
                        principalTable: "LocalFood",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalFoodRecipe_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalFoodRecipe_LocalFoodID",
                table: "LocalFoodRecipe",
                column: "LocalFoodID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalFoodRecipe_RecipeID",
                table: "LocalFoodRecipe",
                column: "RecipeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalFoodRecipe");
        }
    }
}
