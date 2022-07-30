using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firestone.Infrastructure.Data.Migrations
{
    public partial class FirestoneDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FireProgressionTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireProgressionTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetHolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetHolders_FireProgressionTables_TableId",
                        column: x => x.TableId,
                        principalTable: "FireProgressionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FireProgressionTableEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousTableEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetirementTargetValue = table.Column<double>(type: "float", nullable: false),
                    CoastTargetValue = table.Column<double>(type: "float", nullable: false),
                    MinimumGrowthTargetValue = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireProgressionTableEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireProgressionTableEntries_FireProgressionTableEntries_PreviousTableEntryId",
                        column: x => x.PreviousTableEntryId,
                        principalTable: "FireProgressionTableEntries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FireProgressionTableEntries_FireProgressionTables_TableId",
                        column: x => x.TableId,
                        principalTable: "FireProgressionTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InflationRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearlyRate = table.Column<double>(type: "float", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InflationRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InflationRates_FireProgressionTables_TableId",
                        column: x => x.TableId,
                        principalTable: "FireProgressionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NominalReturnRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearlyReturnRate = table.Column<double>(type: "float", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominalReturnRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominalReturnRates_FireProgressionTables_TableId",
                        column: x => x.TableId,
                        principalTable: "FireProgressionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetirementTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearsUntilRetirement = table.Column<int>(type: "int", nullable: false),
                    TargetValue = table.Column<double>(type: "float", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetirementTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetirementTargets_FireProgressionTables_TableId",
                        column: x => x.TableId,
                        principalTable: "FireProgressionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlannedIndividualContributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonthlyIncome = table.Column<double>(type: "float", nullable: false),
                    MonthlyContribution = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedIndividualContributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedIndividualContributions_AssetHolders_AssetHolderId",
                        column: x => x.AssetHolderId,
                        principalTable: "AssetHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualAssetsTotals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualAssetsTotals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualAssetsTotals_AssetHolders_AssetHolderId",
                        column: x => x.AssetHolderId,
                        principalTable: "AssetHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualAssetsTotals_FireProgressionTableEntries_TableEntryId",
                        column: x => x.TableEntryId,
                        principalTable: "FireProgressionTableEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectedAssetsTotals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectionType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectedAssetsTotals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectedAssetsTotals_FireProgressionTableEntries_TableEntryId",
                        column: x => x.TableEntryId,
                        principalTable: "FireProgressionTableEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetHolders_TableId",
                table: "AssetHolders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_FireProgressionTableEntries_PreviousTableEntryId",
                table: "FireProgressionTableEntries",
                column: "PreviousTableEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FireProgressionTableEntries_TableId",
                table: "FireProgressionTableEntries",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualAssetsTotals_AssetHolderId",
                table: "IndividualAssetsTotals",
                column: "AssetHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualAssetsTotals_TableEntryId",
                table: "IndividualAssetsTotals",
                column: "TableEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_InflationRates_TableId",
                table: "InflationRates",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_NominalReturnRates_TableId",
                table: "NominalReturnRates",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedIndividualContributions_AssetHolderId",
                table: "PlannedIndividualContributions",
                column: "AssetHolderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectedAssetsTotals_TableEntryId",
                table: "ProjectedAssetsTotals",
                column: "TableEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_RetirementTargets_TableId",
                table: "RetirementTargets",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualAssetsTotals");

            migrationBuilder.DropTable(
                name: "InflationRates");

            migrationBuilder.DropTable(
                name: "NominalReturnRates");

            migrationBuilder.DropTable(
                name: "PlannedIndividualContributions");

            migrationBuilder.DropTable(
                name: "ProjectedAssetsTotals");

            migrationBuilder.DropTable(
                name: "RetirementTargets");

            migrationBuilder.DropTable(
                name: "AssetHolders");

            migrationBuilder.DropTable(
                name: "FireProgressionTableEntries");

            migrationBuilder.DropTable(
                name: "FireProgressionTables");
        }
    }
}
