using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennisApp.Migrations
{
    public partial class switchfromdateTimeOffsettodateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1Score",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player2Score",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Player2Id",
                table: "Games",
                newName: "PlayerWhoWonId");

            migrationBuilder.RenameColumn(
                name: "Player1Id",
                table: "Games",
                newName: "PlayerWhoLostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerWhoWonId",
                table: "Games",
                newName: "Player2Id");

            migrationBuilder.RenameColumn(
                name: "PlayerWhoLostId",
                table: "Games",
                newName: "Player1Id");

            migrationBuilder.AddColumn<int>(
                name: "Player1Score",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2Score",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
