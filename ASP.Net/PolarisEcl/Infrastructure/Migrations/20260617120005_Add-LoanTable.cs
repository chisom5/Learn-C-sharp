using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolarisEcl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    OutstandingBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductType = table.Column<int>(type: "integer", nullable: false),
                    ECLStage = table.Column<int>(type: "integer", nullable: false),
                    ReportingDate = table.Column<string>(type: "text", nullable: false),
                    LGD = table.Column<decimal>(type: "numeric", nullable: false),
                    PD = table.Column<decimal>(type: "numeric", nullable: false),
                    EAD = table.Column<decimal>(type: "numeric", nullable: false),
                    ECL = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan");
        }
    }
}
