using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusOnTime.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeForeignKeysNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentModel_EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentState_EquipmentStateId",
                table: "EquipmentModelStateHourlyEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentPositionHistory_Equipment_EquipmentId",
                table: "EquipmentPositionHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentStateHistory_EquipmentState_EquipmentStateId",
                table: "EquipmentStateHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentStateHistory_Equipment_EquipmentId",
                table: "EquipmentStateHistory");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentId",
                table: "EquipmentPositionHistory",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentStateId",
                table: "EquipmentModelStateHourlyEarnings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentId",
                table: "EquipmentModel",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentModel_EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings",
                column: "EquipmentModelId",
                principalTable: "EquipmentModel",
                principalColumn: "EquipmentModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentState_EquipmentStateId",
                table: "EquipmentModelStateHourlyEarnings",
                column: "EquipmentStateId",
                principalTable: "EquipmentState",
                principalColumn: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentPositionHistory_Equipment_EquipmentId",
                table: "EquipmentPositionHistory",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentStateHistory_EquipmentState_EquipmentStateId",
                table: "EquipmentStateHistory",
                column: "EquipmentStateId",
                principalTable: "EquipmentState",
                principalColumn: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentStateHistory_Equipment_EquipmentId",
                table: "EquipmentStateHistory",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentModel_EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentState_EquipmentStateId",
                table: "EquipmentModelStateHourlyEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentPositionHistory_Equipment_EquipmentId",
                table: "EquipmentPositionHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentStateHistory_EquipmentState_EquipmentStateId",
                table: "EquipmentStateHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentStateHistory_Equipment_EquipmentId",
                table: "EquipmentStateHistory");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentId",
                table: "EquipmentPositionHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentStateId",
                table: "EquipmentModelStateHourlyEarnings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentId",
                table: "EquipmentModel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentModel_EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings",
                column: "EquipmentModelId",
                principalTable: "EquipmentModel",
                principalColumn: "EquipmentModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModelStateHourlyEarnings_EquipmentState_EquipmentStateId",
                table: "EquipmentModelStateHourlyEarnings",
                column: "EquipmentStateId",
                principalTable: "EquipmentState",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentPositionHistory_Equipment_EquipmentId",
                table: "EquipmentPositionHistory",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentStateHistory_EquipmentState_EquipmentStateId",
                table: "EquipmentStateHistory",
                column: "EquipmentStateId",
                principalTable: "EquipmentState",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentStateHistory_Equipment_EquipmentId",
                table: "EquipmentStateHistory",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
