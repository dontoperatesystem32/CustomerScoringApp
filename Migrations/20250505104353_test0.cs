using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoringSystem_web_api.Migrations
{
    /// <inheritdoc />
    public partial class test0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryCondition_PropertiesJson",
                table: "ConditionStrategies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SalaryCondition_PropertiesJson",
                table: "ConditionStrategies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
