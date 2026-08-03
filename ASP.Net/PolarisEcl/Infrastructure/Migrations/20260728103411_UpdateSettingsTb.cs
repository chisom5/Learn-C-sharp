using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolarisEcl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSettingsTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CollateralTypes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "SegmentType");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CollateralTypes",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SegmentType",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CollateralTypes",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Segments",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CollateralTypes",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
