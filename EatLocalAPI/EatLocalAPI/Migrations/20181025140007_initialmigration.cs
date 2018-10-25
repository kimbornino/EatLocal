using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EatLocalAPI.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Markets",
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
                    table.PrimaryKey("PK_Markets", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markets");
        }
    }
}
