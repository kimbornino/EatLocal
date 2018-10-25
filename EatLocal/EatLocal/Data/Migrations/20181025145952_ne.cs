using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EatLocal.Data.Migrations
{
    public partial class ne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalFoodRecipe_LocalFood_LocalFoodID",
                table: "LocalFoodRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalFoodRecipe_Recipe_RecipeID",
                table: "LocalFoodRecipe");

            migrationBuilder.DropIndex(
                name: "IX_LocalFoodRecipe_LocalFoodID",
                table: "LocalFoodRecipe");

            migrationBuilder.DropIndex(
                name: "IX_LocalFoodRecipe_RecipeID",
                table: "LocalFoodRecipe");

            migrationBuilder.CreateTable(
                name: "LocalMarkets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SeasonOpen = table.Column<DateTime>(nullable: false),
                    SeasonClose = table.Column<DateTime>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    CityStateZip = table.Column<string>(nullable: true),
                    MondayStart = table.Column<int>(nullable: false),
                    MondayEnd = table.Column<int>(nullable: false),
                    TuesdayStart = table.Column<int>(nullable: false),
                    TuesdayEnd = table.Column<int>(nullable: false),
                    WednesayStart = table.Column<int>(nullable: false),
                    WednesdayEnd = table.Column<int>(nullable: false),
                    ThursdayStart = table.Column<int>(nullable: false),
                    ThursdayEnd = table.Column<int>(nullable: false),
                    FridayStart = table.Column<int>(nullable: false),
                    FridayEnd = table.Column<int>(nullable: false),
                    SaturdayStart = table.Column<int>(nullable: false),
                    SaturdayEnd = table.Column<int>(nullable: false),
                    SundayStart = table.Column<int>(nullable: false),
                    SundayEnd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalMarkets", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalMarkets");

            migrationBuilder.CreateIndex(
                name: "IX_LocalFoodRecipe_LocalFoodID",
                table: "LocalFoodRecipe",
                column: "LocalFoodID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalFoodRecipe_RecipeID",
                table: "LocalFoodRecipe",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalFoodRecipe_LocalFood_LocalFoodID",
                table: "LocalFoodRecipe",
                column: "LocalFoodID",
                principalTable: "LocalFood",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalFoodRecipe_Recipe_RecipeID",
                table: "LocalFoodRecipe",
                column: "RecipeID",
                principalTable: "Recipe",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
