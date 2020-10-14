using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cs6065_Homework2.Data.Migrations
{
    public partial class MakeDbSetsProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NflTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 48, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NflTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NflGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    PrimaryPoints = table.Column<int>(nullable: false),
                    SecondaryPoints = table.Column<int>(nullable: false),
                    PrimaryTeamId = table.Column<Guid>(nullable: false),
                    SecondaryTeamId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NflGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NflGames_NflTeams_PrimaryTeamId",
                        column: x => x.PrimaryTeamId,
                        principalTable: "NflTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NflGames_NflTeams_SecondaryTeamId",
                        column: x => x.SecondaryTeamId,
                        principalTable: "NflTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NflQuarterbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 32, nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesStarted = table.Column<int>(nullable: false),
                    PassesCompleted = table.Column<int>(nullable: false),
                    PassesAttempted = table.Column<int>(nullable: false),
                    PassingYardsGained = table.Column<int>(nullable: false),
                    PassingTouchdowns = table.Column<int>(nullable: false),
                    InterceptionsThrown = table.Column<int>(nullable: false),
                    FirstDownsPassing = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NflQuarterbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NflQuarterbacks_NflTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "NflTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NflRunningBacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 32, nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesStarted = table.Column<int>(nullable: false),
                    RushingAttempts = table.Column<int>(nullable: false),
                    RushingYards = table.Column<int>(nullable: false),
                    RushingTouchdowns = table.Column<int>(nullable: false),
                    FirstDownsRushing = table.Column<int>(nullable: false),
                    LongestRushingAttempt = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NflRunningBacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NflRunningBacks_NflTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "NflTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NflTightEnds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 32, nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesStarted = table.Column<int>(nullable: false),
                    PassTargets = table.Column<int>(nullable: false),
                    Receptions = table.Column<int>(nullable: false),
                    ReceivingYards = table.Column<int>(nullable: false),
                    FirstDownsReceiving = table.Column<int>(nullable: false),
                    YardsBeforeCatch = table.Column<int>(nullable: false),
                    YardsAfterCatch = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NflTightEnds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NflTightEnds_NflTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "NflTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NflWideReceivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 32, nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesStarted = table.Column<int>(nullable: false),
                    PassTargets = table.Column<int>(nullable: false),
                    Receptions = table.Column<int>(nullable: false),
                    ReceivingYards = table.Column<int>(nullable: false),
                    ReceivingTouchdowns = table.Column<int>(nullable: false),
                    FirstDownsReceiving = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NflWideReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NflWideReceivers_NflTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "NflTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyRosters",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(nullable: false),
                    QuarterbackId = table.Column<Guid>(nullable: false),
                    RunningBack1Id = table.Column<Guid>(nullable: false),
                    RunningBack2Id = table.Column<Guid>(nullable: false),
                    WideReceiver1Id = table.Column<Guid>(nullable: false),
                    WideReceiver2Id = table.Column<Guid>(nullable: false),
                    TightEndId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyRosters", x => x.OwnerId);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflQuarterbacks_QuarterbackId",
                        column: x => x.QuarterbackId,
                        principalTable: "NflQuarterbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflRunningBacks_RunningBack1Id",
                        column: x => x.RunningBack1Id,
                        principalTable: "NflRunningBacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflRunningBacks_RunningBack2Id",
                        column: x => x.RunningBack2Id,
                        principalTable: "NflRunningBacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflTightEnds_TightEndId",
                        column: x => x.TightEndId,
                        principalTable: "NflTightEnds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflWideReceivers_WideReceiver1Id",
                        column: x => x.WideReceiver1Id,
                        principalTable: "NflWideReceivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflWideReceivers_WideReceiver2Id",
                        column: x => x.WideReceiver2Id,
                        principalTable: "NflWideReceivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FantasyRosters_QuarterbackId",
                table: "FantasyRosters",
                column: "QuarterbackId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyRosters_RunningBack1Id",
                table: "FantasyRosters",
                column: "RunningBack1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyRosters_RunningBack2Id",
                table: "FantasyRosters",
                column: "RunningBack2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyRosters_TightEndId",
                table: "FantasyRosters",
                column: "TightEndId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyRosters_WideReceiver1Id",
                table: "FantasyRosters",
                column: "WideReceiver1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyRosters_WideReceiver2Id",
                table: "FantasyRosters",
                column: "WideReceiver2Id");

            migrationBuilder.CreateIndex(
                name: "IX_NflGames_PrimaryTeamId",
                table: "NflGames",
                column: "PrimaryTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_NflGames_SecondaryTeamId",
                table: "NflGames",
                column: "SecondaryTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_NflQuarterbacks_TeamId",
                table: "NflQuarterbacks",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_NflRunningBacks_TeamId",
                table: "NflRunningBacks",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_NflTightEnds_TeamId",
                table: "NflTightEnds",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_NflWideReceivers_TeamId",
                table: "NflWideReceivers",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FantasyRosters");

            migrationBuilder.DropTable(
                name: "NflGames");

            migrationBuilder.DropTable(
                name: "NflQuarterbacks");

            migrationBuilder.DropTable(
                name: "NflRunningBacks");

            migrationBuilder.DropTable(
                name: "NflTightEnds");

            migrationBuilder.DropTable(
                name: "NflWideReceivers");

            migrationBuilder.DropTable(
                name: "NflTeams");
        }
    }
}
