using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolarisEcl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Month = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    AccountNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    OutstandingBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    Stage = table.Column<int>(type: "integer", nullable: false),
                    ProductType = table.Column<string>(type: "text", nullable: false),
                    PD = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    LGD = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    EAD = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ECL = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });

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
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
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
                    ComputationName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ReportingPeriod = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ArchivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ComputedById = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorizeById = table.Column<Guid>(type: "uuid", nullable: true),
                    ReviewComment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PdWeightBaseline = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PdWeightBestcase = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PdWeightWorstcase = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
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
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputationFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    File = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    StoragePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UploadedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputationFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputationFiles_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputationFiles_Users_UploadedById",
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
                    Dpd = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
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
                name: "ECLReports",
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
                    table.PrimaryKey("PK_ECLReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECLReports_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGDResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectorName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    LgdValue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGDResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGDResults_ECLComputations_ComputationId",
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
                    SectorName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
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
                name: "StageOverrides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoanId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreviousStage = table.Column<int>(type: "integer", nullable: false),
                    NewStage = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageOverrides_ECLComputations_ComputationId",
                        column: x => x.ComputationId,
                        principalTable: "ECLComputations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StageOverrides_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StageOverrides_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EclReportRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dimension = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProductLabel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ECL = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    EAD = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CoverageRatio = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EclReportRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EclReportRows_ECLReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "ECLReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputationFiles_ComputationId",
                table: "ComputationFiles",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputationFiles_UploadedById",
                table: "ComputationFiles",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_EADResults_ComputationId_AccountNo",
                table: "EADResults",
                columns: new[] { "ComputationId", "AccountNo" });

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_AuthorizeById",
                table: "ECLComputations",
                column: "AuthorizeById");

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_ComputedById",
                table: "ECLComputations",
                column: "ComputedById");

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_ReportingPeriod",
                table: "ECLComputations",
                column: "ReportingPeriod");

            migrationBuilder.CreateIndex(
                name: "IX_ECLComputations_Status",
                table: "ECLComputations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_EclReportRows_ReportId",
                table: "EclReportRows",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ECLReports_ComputationId",
                table: "ECLReports",
                column: "ComputationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LGDResults_ComputationId",
                table: "LGDResults",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_PDResults_ComputationId",
                table: "PDResults",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StageOverrides_ComputationId",
                table: "StageOverrides",
                column: "ComputationId");

            migrationBuilder.CreateIndex(
                name: "IX_StageOverrides_CreatedById",
                table: "StageOverrides",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StageOverrides_LoanId",
                table: "StageOverrides",
                column: "LoanId");

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
                name: "ComputationFiles");

            migrationBuilder.DropTable(
                name: "EADResults");

            migrationBuilder.DropTable(
                name: "EclReportRows");

            migrationBuilder.DropTable(
                name: "LGDResults");

            migrationBuilder.DropTable(
                name: "PDResults");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "StageOverrides");

            migrationBuilder.DropTable(
                name: "ECLReports");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "ECLComputations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
