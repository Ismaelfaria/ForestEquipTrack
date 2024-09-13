using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestEquipTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EquipmentModelName",
                table: "EquipmentModelStateHourlyEarnings",
                newName: "ModelName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModelName",
                table: "EquipmentModelStateHourlyEarnings",
                newName: "EquipmentModelName");
        }
    }
}
