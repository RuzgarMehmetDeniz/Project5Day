using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project5Day.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchEvents",
                columns: table => new
                {
                    MatchEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsHomeTeam = table.Column<bool>(type: "bit", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerOut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvents", x => x.MatchEventId);
                    table.ForeignKey(
                        name: "FK_MatchEvents_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchStatistics",
                columns: table => new
                {
                    MatchStatisticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    HomePossession = table.Column<int>(type: "int", nullable: false),
                    HomeShots = table.Column<int>(type: "int", nullable: false),
                    HomeShotsOnTarget = table.Column<int>(type: "int", nullable: false),
                    HomePasses = table.Column<int>(type: "int", nullable: false),
                    HomePassAccuracy = table.Column<int>(type: "int", nullable: false),
                    HomeCorners = table.Column<int>(type: "int", nullable: false),
                    HomeFouls = table.Column<int>(type: "int", nullable: false),
                    HomeOffsides = table.Column<int>(type: "int", nullable: false),
                    AwayPossession = table.Column<int>(type: "int", nullable: false),
                    AwayShots = table.Column<int>(type: "int", nullable: false),
                    AwayShotsOnTarget = table.Column<int>(type: "int", nullable: false),
                    AwayPasses = table.Column<int>(type: "int", nullable: false),
                    AwayPassAccuracy = table.Column<int>(type: "int", nullable: false),
                    AwayCorners = table.Column<int>(type: "int", nullable: false),
                    AwayFouls = table.Column<int>(type: "int", nullable: false),
                    AwayOffsides = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStatistics", x => x.MatchStatisticId);
                    table.ForeignKey(
                        name: "FK_MatchStatistics_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchEvents_MatchId",
                table: "MatchEvents",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStatistics_MatchId",
                table: "MatchStatistics",
                column: "MatchId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchEvents");

            migrationBuilder.DropTable(
                name: "MatchStatistics");
        }
    }
}
