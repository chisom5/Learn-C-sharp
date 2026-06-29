using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolarisEcl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECLComputations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ReportingPeriod = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArchivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ComputedById = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorizeById = table.Column<Guid>(type: "uuid", nullable: true),
                    ReviewComment = table.Column<string>(type: "text", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PdWeightBaseline = table.Column<int>(type: "integer", nullable: false),
                    PdWeightBestcase = table.Column<int>(type: "integer", nullable: false),
                    PdWeightWorstcase = table.Column<int>(type: "integer", nullable: false),
                    HistoricalMargin = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MacroeconomicAdjustmentFactor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECLComputations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECLComputations_Users_AuthorizeById",
                        column: x => x.AuthorizeById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECLComputations_Users_ComputedById",
                        column: x => x.ComputedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComputationFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    File = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    StoragePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploadedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputationFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputationFile_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputationFile_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EADResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Products = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AmortizedCost = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    LoanType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Dpd = table.Column<int>(type: "integer", nullable: false),
                    EIROrCommercialRate = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MaturityDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SavingsCollateral = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month1Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month2Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month3Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month4Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month5Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month6Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month7Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month8Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month9Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month10Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month11Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Month12Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EADResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EADResults_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ECLReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportStatus = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    TotalECL = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalEAD = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalCoverageRatio = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECLReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECLReport_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGDResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectorName = table.Column<string>(type: "text", nullable: false),
                    LgdValue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGDResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGDResult_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PDResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectorName = table.Column<string>(type: "text", nullable: false),
                    Month1Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month2Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month3Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month4Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month5Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month6Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month7Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month8Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month9Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month10Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month11Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month12Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PDResults_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EclReportRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dimension = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProductLabel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ECL = table.Column<long>(type: "bigint", nullable: false),
                    EAD = table.Column<long>(type: "bigint", nullable: false),
                    CoverageRatio = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EclReportRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EclReportRow_ECLReport_ReportId",
                        column: x => x.ReportId,
                        principalTable: "ECLReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputationFile_ComputationId",
                table: "ComputationFile",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputationFile_UploadedById",
                table: "ComputationFile",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_EADResults_ComputationId",
                table: "EADResults",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_AuthorizeById",
                table: "ECLComputations",
                column: "AuthorizeById");

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_ComputedById",
                table: "ECLComputations",
                column: "ComputedById");

            migrationBuilder.CreateIndex(
                name: "IX_ECLReport_ComputationId",
                table: "ECLReport",
                column: "ComputationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EclReportRow_ReportId",
                table: "EclReportRow",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_LGDResult_ComputationId",
                table: "LGDResult",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_PDResults_ComputationId",
                table: "PDResults",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputationFile");

            migrationBuilder.DropTable(
                name: "EADResults");

            migrationBuilder.DropTable(
                name: "EclReportRow");

            migrationBuilder.DropTable(
                name: "LGDResult");

            migrationBuilder.DropTable(
                name: "PDResults");

            migrationBuilder.DropTable(
                name: "ECLReport");

            migrationBuilder.DropTable(
                name: "ECLComputations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
