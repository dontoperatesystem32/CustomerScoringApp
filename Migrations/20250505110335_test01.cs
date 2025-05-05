using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoringSystem_web_api.Migrations
{
    /// <inheritdoc />
    public partial class test01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertiesJson",
                table: "ConditionStrategies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertiesJson",
                table: "ConditionStrategies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
