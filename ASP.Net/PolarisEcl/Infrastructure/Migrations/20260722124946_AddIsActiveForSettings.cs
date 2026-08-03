using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolarisEcl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveForSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RegressionParameters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ExpectedCorrelations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RegressionParameters");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ExpectedCorrelations");
        }
    }
}
