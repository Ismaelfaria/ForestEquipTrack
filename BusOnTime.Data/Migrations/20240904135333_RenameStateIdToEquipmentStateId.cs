using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusOnTime.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameStateIdToEquipmentStateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentStateHistory",
                table: "EquipmentStateHistory");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "EquipmentState",
                newName: "EquipmentStateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentStateId",
                table: "EquipmentStateHistory",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentId",
                table: "EquipmentStateHistory",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentStateHistoryId",
                table: "EquipmentStateHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentStateHistory",
                table: "EquipmentStateHistory",
                column: "EquipmentStateHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentStateHistory_EquipmentId",
                table: "EquipmentStateHistory",
                column: "EquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentStateHistory",
                table: "EquipmentStateHistory");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentStateHistory_EquipmentId",
                table: "EquipmentStateHistory");

            migrationBuilder.DropColumn(
                name: "EquipmentStateHistoryId",
                table: "EquipmentStateHistory");

            migrationBuilder.RenameColumn(
                name: "EquipmentStateId",
                table: "EquipmentState",
                newName: "StateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentStateId",
                table: "EquipmentStateHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentId",
                table: "EquipmentStateHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentStateHistory",
                table: "EquipmentStateHistory",
                columns: new[] { "EquipmentId", "Date" });
        }
    }
}
