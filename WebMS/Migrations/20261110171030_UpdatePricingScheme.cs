using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePricingScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "SeatSections",
                newName: "PriceModifier");

            migrationBuilder.AddColumn<int>(
                name: "ShowtimeTier",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TimeMultiplier",
                table: "Schedules",
                type: "decimal(4,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "Movies",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowtimeTier",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TimeMultiplier",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "PriceModifier",
                table: "SeatSections",
                newName: "BasePrice");
        }
    }
}
