using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusOnTime.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentModel",
                columns: table => new
                {
                    EquipmentModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentModel", x => x.EquipmentModelId);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentState",
                columns: table => new
                {
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentState", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    EquipmentModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentStateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentModelStateHourlyEarnings", x => x.EquipmentModelStateHourlyEarningsId);
                    table.ForeignKey(
                        name: "FK_EquipmentModelStateHourlyEarnings_EquipmentModel_EquipmentModelId",
                        column: x => x.EquipmentModelId,
                        principalTable: "EquipmentModel",
                        principalColumn: "EquipmentModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentModelStateHourlyEarnings_EquipmentState_EquipmentStateId",
                        column: x => x.EquipmentStateId,
                        principalTable: "EquipmentState",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPositionHistory",
                columns: table => new
                {
                    EquipmentPositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lat = table.Column<int>(type: "int", nullable: false),
                    Lon = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPositionHistory", x => x.EquipmentPositionId);
                    table.ForeignKey(
                        name: "FK_EquipmentPositionHistory_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentStateHistory",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipmentStateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentStateHistory", x => new { x.EquipmentId, x.Date });
                    table.ForeignKey(
                        name: "FK_EquipmentStateHistory_EquipmentState_EquipmentStateId",
                        column: x => x.EquipmentStateId,
                        principalTable: "EquipmentState",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentStateHistory_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_EquipmentModelStateHourlyEarnings_EquipmentStateId",
                table: "EquipmentModelStateHourlyEarnings",
                column: "EquipmentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPositionHistory_EquipmentId",
                table: "EquipmentPositionHistory",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentStateHistory_EquipmentStateId",
                table: "EquipmentStateHistory",
                column: "EquipmentStateId");
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
                name: "EquipmentState");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentModel");
        }
    }
}
