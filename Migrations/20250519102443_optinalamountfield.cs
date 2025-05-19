using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoringSystem_web_api.Migrations
{
    /// <inheritdoc />
    public partial class optinalamountfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OptionalAmount",
                table: "ScoringConditionHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionalAmount",
                table: "ScoringConditionHistory");
        }
    }
}
