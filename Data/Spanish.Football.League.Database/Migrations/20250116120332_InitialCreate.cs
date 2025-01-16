using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Spanish.Football.League.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "spanish_football_league");

            migrationBuilder.CreateTable(
                name: "seasons",
                schema: "spanish_football_league",
                columns: table => new
                {
                    season_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_year = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_seasons", x => x.season_id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "spanish_football_league",
                columns: table => new
                {
                    team_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    weight = table.Column<decimal>(type: "numeric", nullable: false),
                    color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => x.team_id);
                    table.UniqueConstraint("ak_teams_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "spanish_football_league",
                columns: table => new
                {
                    match_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_id = table.Column<int>(type: "integer", nullable: false),
                    season_half = table.Column<int>(type: "integer", nullable: false),
                    home_team_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    home_team_odd = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false),
                    away_team_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    away_team_odd = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matches", x => x.match_id);
                    table.ForeignKey(
                        name: "fk_matches_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "spanish_football_league",
                        principalTable: "seasons",
                        principalColumn: "season_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_matches_teams_away_team_name",
                        column: x => x.away_team_name,
                        principalSchema: "spanish_football_league",
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_matches_teams_home_team_name",
                        column: x => x.home_team_name,
                        principalSchema: "spanish_football_league",
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "results",
                schema: "spanish_football_league",
                columns: table => new
                {
                    result_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_id = table.Column<int>(type: "integer", nullable: false),
                    season_half = table.Column<int>(type: "integer", nullable: false),
                    home_team_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    away_team_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    home_team_score = table.Column<int>(type: "integer", nullable: false),
                    away_team_score = table.Column<int>(type: "integer", nullable: false),
                    is_expected = table.Column<bool>(type: "boolean", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_results", x => x.result_id);
                    table.ForeignKey(
                        name: "fk_results_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "spanish_football_league",
                        principalTable: "seasons",
                        principalColumn: "season_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_results_teams_away_team_name",
                        column: x => x.away_team_name,
                        principalSchema: "spanish_football_league",
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_results_teams_home_team_name",
                        column: x => x.home_team_name,
                        principalSchema: "spanish_football_league",
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "season_stats",
                schema: "spanish_football_league",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_id = table.Column<int>(type: "integer", nullable: false),
                    team_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    scored_goals = table.Column<int>(type: "integer", nullable: false),
                    conceded_goals = table.Column<int>(type: "integer", nullable: false),
                    goal_difference = table.Column<int>(type: "integer", nullable: false),
                    points = table.Column<int>(type: "integer", nullable: false),
                    wins = table.Column<int>(type: "integer", nullable: false),
                    draws = table.Column<int>(type: "integer", nullable: false),
                    losses = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_season_stats", x => x.id);
                    table.ForeignKey(
                        name: "fk_season_stats_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "spanish_football_league",
                        principalTable: "seasons",
                        principalColumn: "season_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_season_stats_teams_team_name",
                        column: x => x.team_name,
                        principalSchema: "spanish_football_league",
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "team_details",
                schema: "spanish_football_league",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_id = table.Column<int>(type: "integer", nullable: false),
                    team_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    weight = table.Column<decimal>(type: "numeric", nullable: false),
                    expected_win_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_team_details_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "spanish_football_league",
                        principalTable: "seasons",
                        principalColumn: "season_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_team_details_teams_team_name",
                        column: x => x.team_name,
                        principalSchema: "spanish_football_league",
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "winners",
                schema: "spanish_football_league",
                columns: table => new
                {
                    winner_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_id = table.Column<int>(type: "integer", nullable: false),
                    winner_team_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    points = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_winners", x => x.winner_id);
                    table.ForeignKey(
                        name: "fk_winners_teams_winner_team_name",
                        column: x => x.winner_team_name,
                        principalSchema: "spanish_football_league",
                        principalTable: "teams",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_matches_away_team_name",
                schema: "spanish_football_league",
                table: "matches",
                column: "away_team_name");

            migrationBuilder.CreateIndex(
                name: "ix_matches_home_team_name",
                schema: "spanish_football_league",
                table: "matches",
                column: "home_team_name");

            migrationBuilder.CreateIndex(
                name: "ix_matches_season_id",
                schema: "spanish_football_league",
                table: "matches",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_results_away_team_name",
                schema: "spanish_football_league",
                table: "results",
                column: "away_team_name");

            migrationBuilder.CreateIndex(
                name: "ix_results_home_team_name",
                schema: "spanish_football_league",
                table: "results",
                column: "home_team_name");

            migrationBuilder.CreateIndex(
                name: "ix_results_season_id",
                schema: "spanish_football_league",
                table: "results",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_season_stats_season_id",
                schema: "spanish_football_league",
                table: "season_stats",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_season_stats_team_name",
                schema: "spanish_football_league",
                table: "season_stats",
                column: "team_name");

            migrationBuilder.CreateIndex(
                name: "ix_team_details_season_id",
                schema: "spanish_football_league",
                table: "team_details",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_team_details_team_name",
                schema: "spanish_football_league",
                table: "team_details",
                column: "team_name");

            migrationBuilder.CreateIndex(
                name: "ix_winners_winner_team_name",
                schema: "spanish_football_league",
                table: "winners",
                column: "winner_team_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches",
                schema: "spanish_football_league");

            migrationBuilder.DropTable(
                name: "results",
                schema: "spanish_football_league");

            migrationBuilder.DropTable(
                name: "season_stats",
                schema: "spanish_football_league");

            migrationBuilder.DropTable(
                name: "team_details",
                schema: "spanish_football_league");

            migrationBuilder.DropTable(
                name: "winners",
                schema: "spanish_football_league");

            migrationBuilder.DropTable(
                name: "seasons",
                schema: "spanish_football_league");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "spanish_football_league");
        }
    }
}
