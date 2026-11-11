using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHallSeatLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Halls_SeatLayoutId",
                table: "Halls");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatLayoutId",
                table: "Halls",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_SeatLayoutId",
                table: "Halls",
                column: "SeatLayoutId",
                unique: true,
                filter: "[SeatLayoutId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_SeatLayouts_SeatLayoutId",
                table: "Halls",
                column: "SeatLayoutId",
                principalTable: "SeatLayouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_SeatLayouts_SeatLayoutId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_SeatLayoutId",
                table: "Halls");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeatLayoutId",
                table: "Halls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Halls_SeatLayoutId",
                table: "Halls",
                column: "SeatLayoutId",
                unique: true);
        }
    }
}
