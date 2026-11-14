using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedBannerAndCinemaImagesToDBcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaImage_Cinemas_CinemaId",
                table: "CinemaImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaImage",
                table: "CinemaImage");

            migrationBuilder.RenameTable(
                name: "CinemaImage",
                newName: "CinemaImages");

            migrationBuilder.RenameIndex(
                name: "IX_CinemaImage_CinemaId",
                table: "CinemaImages",
                newName: "IX_CinemaImages_CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaImages",
                table: "CinemaImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaImages_Cinemas_CinemaId",
                table: "CinemaImages",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaImages_Cinemas_CinemaId",
                table: "CinemaImages");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaImages",
                table: "CinemaImages");

            migrationBuilder.RenameTable(
                name: "CinemaImages",
                newName: "CinemaImage");

            migrationBuilder.RenameIndex(
                name: "IX_CinemaImages_CinemaId",
                table: "CinemaImage",
                newName: "IX_CinemaImage_CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaImage",
                table: "CinemaImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaImage_Cinemas_CinemaId",
                table: "CinemaImage",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
