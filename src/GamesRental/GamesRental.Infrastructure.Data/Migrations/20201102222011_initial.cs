using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesRental.Infrastructure.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(180)", nullable: false),
                    Email = table.Column<string>(type: "varchar(180)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(180)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "dateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "varchar(180)", nullable: false),
                    Name = table.Column<string>(type: "varchar(180)", nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(type: "varchar(180)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateEstimated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateFinish = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdFriend = table.Column<int>(nullable: false),
                    IdGame = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Friends_IdFriend",
                        column: x => x.IdFriend,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Games_IdGame",
                        column: x => x.IdGame,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_IdFriend",
                table: "Rents",
                column: "IdFriend");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_IdGame",
                table: "Rents",
                column: "IdGame");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
