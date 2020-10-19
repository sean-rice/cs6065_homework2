using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cs6065_Homework2.Data.Migrations
{
    public partial class InitialCreateAzureSql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_NflGames_NflTeams_SecondaryTeamId",
                        column: x => x.SecondaryTeamId,
                        principalTable: "NflTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                    TightEndId = table.Column<Guid>(nullable: false),
                    WideReceiver1Id = table.Column<Guid>(nullable: false),
                    WideReceiver2Id = table.Column<Guid>(nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflRunningBacks_RunningBack1Id",
                        column: x => x.RunningBack1Id,
                        principalTable: "NflRunningBacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflRunningBacks_RunningBack2Id",
                        column: x => x.RunningBack2Id,
                        principalTable: "NflRunningBacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflTightEnds_TightEndId",
                        column: x => x.TightEndId,
                        principalTable: "NflTightEnds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflWideReceivers_WideReceiver1Id",
                        column: x => x.WideReceiver1Id,
                        principalTable: "NflWideReceivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FantasyRosters_NflWideReceivers_WideReceiver2Id",
                        column: x => x.WideReceiver2Id,
                        principalTable: "NflWideReceivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FantasyRosters");

            migrationBuilder.DropTable(
                name: "NflGames");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
