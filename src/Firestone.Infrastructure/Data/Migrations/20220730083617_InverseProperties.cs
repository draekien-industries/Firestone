using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firestone.Infrastructure.Data.Migrations
{
    public partial class InverseProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetHolders_FireProgressionTables_TableId",
                table: "AssetHolders");

            migrationBuilder.DropIndex(
                name: "IX_RetirementTargets_TableId",
                table: "RetirementTargets");

            migrationBuilder.DropIndex(
                name: "IX_PlannedIndividualContributions_AssetHolderId",
                table: "PlannedIndividualContributions");

            migrationBuilder.DropIndex(
                name: "IX_NominalReturnRates_TableId",
                table: "NominalReturnRates");

            migrationBuilder.DropIndex(
                name: "IX_InflationRates_TableId",
                table: "InflationRates");

            migrationBuilder.DropIndex(
                name: "IX_AssetHolders_TableId",
                table: "AssetHolders");

            migrationBuilder.AddColumn<Guid>(
                name: "FireProgressionTableId",
                table: "AssetHolders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetirementTargets_TableId",
                table: "RetirementTargets",
                column: "TableId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlannedIndividualContributions_AssetHolderId",
                table: "PlannedIndividualContributions",
                column: "AssetHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_NominalReturnRates_TableId",
                table: "NominalReturnRates",
                column: "TableId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InflationRates_TableId",
                table: "InflationRates",
                column: "TableId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetHolders_FireProgressionTableId",
                table: "AssetHolders",
                column: "FireProgressionTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHolders_FireProgressionTables_FireProgressionTableId",
                table: "AssetHolders",
                column: "FireProgressionTableId",
                principalTable: "FireProgressionTables",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetHolders_FireProgressionTables_FireProgressionTableId",
                table: "AssetHolders");

            migrationBuilder.DropIndex(
                name: "IX_RetirementTargets_TableId",
                table: "RetirementTargets");

            migrationBuilder.DropIndex(
                name: "IX_PlannedIndividualContributions_AssetHolderId",
                table: "PlannedIndividualContributions");

            migrationBuilder.DropIndex(
                name: "IX_NominalReturnRates_TableId",
                table: "NominalReturnRates");

            migrationBuilder.DropIndex(
                name: "IX_InflationRates_TableId",
                table: "InflationRates");

            migrationBuilder.DropIndex(
                name: "IX_AssetHolders_FireProgressionTableId",
                table: "AssetHolders");

            migrationBuilder.DropColumn(
                name: "FireProgressionTableId",
                table: "AssetHolders");

            migrationBuilder.CreateIndex(
                name: "IX_RetirementTargets_TableId",
                table: "RetirementTargets",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedIndividualContributions_AssetHolderId",
                table: "PlannedIndividualContributions",
                column: "AssetHolderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NominalReturnRates_TableId",
                table: "NominalReturnRates",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_InflationRates_TableId",
                table: "InflationRates",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetHolders_TableId",
                table: "AssetHolders",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHolders_FireProgressionTables_TableId",
                table: "AssetHolders",
                column: "TableId",
                principalTable: "FireProgressionTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
