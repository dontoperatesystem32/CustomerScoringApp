using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoringSystem_web_api.Migrations
{
    /// <inheritdoc />
    public partial class ScoringHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoringHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluatedCustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoringPassed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoringHistory_Customers_EvaluatedCustomerId",
                        column: x => x.EvaluatedCustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringConditionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoringRequestId = table.Column<int>(type: "int", nullable: false),
                    EvaluatedConditionId = table.Column<int>(type: "int", nullable: false),
                    EvaluationResult = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringConditionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoringConditionHistory_ConditionStrategies_EvaluatedConditionId",
                        column: x => x.EvaluatedConditionId,
                        principalTable: "ConditionStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoringConditionHistory_ScoringHistory_ScoringRequestId",
                        column: x => x.ScoringRequestId,
                        principalTable: "ScoringHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoringConditionHistory_EvaluatedConditionId",
                table: "ScoringConditionHistory",
                column: "EvaluatedConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringConditionHistory_ScoringRequestId",
                table: "ScoringConditionHistory",
                column: "ScoringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringHistory_EvaluatedCustomerId",
                table: "ScoringHistory",
                column: "EvaluatedCustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoringConditionHistory");

            migrationBuilder.DropTable(
                name: "ScoringHistory");
        }
    }
}
