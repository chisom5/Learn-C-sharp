using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolarisEcl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SettingsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ECLComputations_ReportingPeriod",
                table: "ECLComputations");

            migrationBuilder.DropColumn(
                name: "ReportingPeriod",
                table: "ECLComputations");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportingEndDate",
                table: "ECLComputations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportingStartDate",
                table: "ECLComputations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "CollateralTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    HaircutPercent = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Perfected = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DurationYears = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollateralTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollateralTypes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpectedCorrelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrimeLendingRate = table.Column<int>(type: "integer", nullable: false),
                    Inflation = table.Column<int>(type: "integer", nullable: false),
                    YieldOnTreasuryBills = table.Column<int>(type: "integer", nullable: false),
                    ExchangeRate = table.Column<int>(type: "integer", nullable: false),
                    MonetaryPolicyRate = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedCorrelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpectedCorrelations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegressionParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RSquareLimit = table.Column<decimal>(type: "numeric", nullable: false),
                    PValueSelected = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegressionParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegressionParameters_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segments_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_ReportingEndDate",
                table: "ECLComputations",
                column: "ReportingEndDate");

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_ReportingStartDate",
                table: "ECLComputations",
                column: "ReportingStartDate");

            migrationBuilder.CreateIndex(
                name: "IX_CollateralTypes_UpdatedById",
                table: "CollateralTypes",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedCorrelations_UpdatedById",
                table: "ExpectedCorrelations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedById",
                table: "Products",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegressionParameters_UpdatedById",
                table: "RegressionParameters",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_UpdatedById",
                table: "Segments",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollateralTypes");

            migrationBuilder.DropTable(
                name: "ExpectedCorrelations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RegressionParameters");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropIndex(
                name: "IX_ECLComputations_ReportingEndDate",
                table: "ECLComputations");

            migrationBuilder.DropIndex(
                name: "IX_ECLComputations_ReportingStartDate",
                table: "ECLComputations");

            migrationBuilder.DropColumn(
                name: "ReportingEndDate",
                table: "ECLComputations");

            migrationBuilder.DropColumn(
                name: "ReportingStartDate",
                table: "ECLComputations");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportingPeriod",
                table: "ECLComputations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_ReportingPeriod",
                table: "ECLComputations",
                column: "ReportingPeriod");
        }
    }
}
