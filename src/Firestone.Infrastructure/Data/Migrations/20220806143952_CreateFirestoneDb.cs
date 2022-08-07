using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firestone.Infrastructure.Data.Migrations
{
    public partial class CreateFirestoneDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FireTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearlyInflationRate = table.Column<double>(type: "float", nullable: false),
                    YearlyNominalReturnRate = table.Column<double>(type: "float", nullable: false),
                    RetirementTargetBeforeInflation = table.Column<double>(type: "float", nullable: false),
                    YearsToRetirement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetHolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FireTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    ExpectedMonthlyIncome = table.Column<double>(type: "float", nullable: false),
                    PlannedMonthlyContribution = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetHolders_FireTables_FireTableId",
                        column: x => x.FireTableId,
                        principalTable: "FireTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FireTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItems_FireTables_FireTableId",
                        column: x => x.FireTableId,
                        principalTable: "FireTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetHolders_AssetHolderId",
                        column: x => x.AssetHolderId,
                        principalTable: "AssetHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_LineItems_LineItemId",
                        column: x => x.LineItemId,
                        principalTable: "LineItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetHolders_FireTableId",
                table: "AssetHolders",
                column: "FireTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetHolderId",
                table: "Assets",
                column: "AssetHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LineItemId",
                table: "Assets",
                column: "LineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_FireTableId",
                table: "LineItems",
                column: "FireTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetHolders");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "FireTables");
        }
    }
}
