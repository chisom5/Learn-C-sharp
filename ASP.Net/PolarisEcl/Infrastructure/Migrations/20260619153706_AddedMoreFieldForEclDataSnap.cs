using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolarisEcl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreFieldForEclDataSnap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReportingPeriod",
                table: "ECLComputations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "EclDataSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    SnapshotDate = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    OutstandingBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    Stage = table.Column<int>(type: "integer", nullable: false),
                    ProductType = table.Column<int>(type: "integer", nullable: false),
                    PD = table.Column<decimal>(type: "numeric", nullable: false),
                    LGD = table.Column<decimal>(type: "numeric", nullable: false),
                    EAD = table.Column<decimal>(type: "numeric", nullable: false),
                    ECL = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EclDataSnapshot", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EclDataSnapshot");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReportingPeriod",
                table: "ECLComputations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    EAD = table.Column<decimal>(type: "numeric", nullable: false),
                    ECL = table.Column<decimal>(type: "numeric", nullable: false),
                    ECLStage = table.Column<int>(type: "integer", nullable: false),
                    LGD = table.Column<decimal>(type: "numeric", nullable: false),
                    OutstandingBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    PD = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductType = table.Column<int>(type: "integer", nullable: false),
                    ReportingDate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                });
        }
    }
}
