using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestEquipTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnumStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentModel",
                columns: table => new
                {
                    EquipmentModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentModel", x => x.EquipmentModelId);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipmentModel_EquipmentModelId",
                        column: x => x.EquipmentModelId,
                        principalTable: "EquipmentModel",
                        principalColumn: "EquipmentModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentModelStateHourlyEarnings",
                columns: table => new
                {
                    EquipmentModelStateHourlyEarningsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentModelStateHourlyEarnings", x => x.EquipmentModelStateHourlyEarningsId);
                    table.ForeignKey(
                        name: "FK_EquipmentModelStateHourlyEarnings_EquipmentModel_EquipmentModelId",
                        column: x => x.EquipmentModelId,
                        principalTable: "EquipmentModel",
                        principalColumn: "EquipmentModelId");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPositionHistory",
                columns: table => new
                {
                    EquipmentPositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPositionHistory", x => x.EquipmentPositionId);
                    table.ForeignKey(
                        name: "FK_EquipmentPositionHistory_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentStateHistory",
                columns: table => new
                {
                    EquipmentStateHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentStateHistory", x => x.EquipmentStateHistoryId);
                    table.ForeignKey(
                        name: "FK_EquipmentStateHistory_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentModelId",
                table: "Equipment",
                column: "EquipmentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentModelStateHourlyEarnings_EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings",
                column: "EquipmentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPositionHistory_EquipmentId",
                table: "EquipmentPositionHistory",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentStateHistory_EquipmentId",
                table: "EquipmentStateHistory",
                column: "EquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentModelStateHourlyEarnings");

            migrationBuilder.DropTable(
                name: "EquipmentPositionHistory");

            migrationBuilder.DropTable(
                name: "EquipmentStateHistory");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentModel");
        }
    }
}
