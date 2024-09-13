using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestEquipTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EquipmentModelName",
                table: "EquipmentModelStateHourlyEarnings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipmentModelName",
                table: "EquipmentModelStateHourlyEarnings");

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipmentModelId",
                table: "EquipmentModelStateHourlyEarnings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
