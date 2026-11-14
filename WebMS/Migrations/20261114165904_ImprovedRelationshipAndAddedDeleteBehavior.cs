using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMS.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedRelationshipAndAddedDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_FnbCarts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_FnbProducts_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Merchants_MerchantId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Users_CreatedByUserId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Users_UpdatedByUserId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMovies_Movies_MovieId",
                table: "FavoriteMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Users_CreatedByUserId",
                table: "Halls");

            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Users_UpdatedByUserId",
                table: "Halls");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieTypes_MovieTypeId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_CreatedByUserId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTickets_Users_StaffId",
                table: "MovieTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTypes_Users_CreatedByUserId",
                table: "MovieTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTypes_Users_UpdatedByUserId",
                table: "MovieTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_MovieTypes_CreatedByUserId",
                table: "MovieTypes");

            migrationBuilder.DropIndex(
                name: "IX_MovieTypes_UpdatedByUserId",
                table: "MovieTypes");

            migrationBuilder.DropIndex(
                name: "IX_Halls_UpdatedByUserId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_UpdatedByUserId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "MovieTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "MovieTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Cinemas");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "MovieTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "MovieTypeId",
                table: "Movies",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Movies",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_MovieTypeId",
                table: "Movies",
                newName: "IX_Movies_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_CreatedByUserId",
                table: "Movies",
                newName: "IX_Movies_UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartItems",
                newName: "FnbProductId");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItems",
                newName: "FnbCartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_FnbProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_FnbCartId_FnbProductId");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Transactions",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxAmount",
                table: "Transactions",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "Transactions",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CinemaId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "CinemaNameSnapshot",
                table: "Transactions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceModifier",
                table: "SeatSections",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MovieTypes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MovieTickets",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TrailerPath",
                table: "Movies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Movies",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Movies",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Movies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "MovieImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "FnbProducts",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "FnbOrderItems",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "FnbOrderItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SessionKey",
                table: "FnbCarts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MovieTitleSnapshot",
                table: "FavoriteMovies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "CinemaImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    CinemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaImage_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CreatedByUserId",
                table: "Schedules",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaImage_CinemaId",
                table: "CinemaImage",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_FnbCarts_FnbCartId",
                table: "CartItems",
                column: "FnbCartId",
                principalTable: "FnbCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_FnbProducts_FnbProductId",
                table: "CartItems",
                column: "FnbProductId",
                principalTable: "FnbProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Merchants_MerchantId",
                table: "Cinemas",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Users_CreatedByUserId",
                table: "Cinemas",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMovies_Movies_MovieId",
                table: "FavoriteMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Users_CreatedByUserId",
                table: "Halls",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieTypes_TypeId",
                table: "Movies",
                column: "TypeId",
                principalTable: "MovieTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_UpdatedByUserId",
                table: "Movies",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTickets_Users_StaffId",
                table: "MovieTickets",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_CreatedByUserId",
                table: "Schedules",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cinemas_CinemaId",
                table: "Transactions",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_FnbCarts_FnbCartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_FnbProducts_FnbProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Merchants_MerchantId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Users_CreatedByUserId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMovies_Movies_MovieId",
                table: "FavoriteMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Users_CreatedByUserId",
                table: "Halls");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieTypes_TypeId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_UpdatedByUserId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTickets_Users_StaffId",
                table: "MovieTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_CreatedByUserId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cinemas_CinemaId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "CinemaImage");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CreatedByUserId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CinemaNameSnapshot",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "MovieTypes",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "Movies",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Movies",
                newName: "MovieTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_UpdatedByUserId",
                table: "Movies",
                newName: "IX_Movies_CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_TypeId",
                table: "Movies",
                newName: "IX_Movies_MovieTypeId");

            migrationBuilder.RenameColumn(
                name: "FnbProductId",
                table: "CartItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "FnbCartId",
                table: "CartItems",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_FnbProductId",
                table: "CartItems",
                newName: "IX_CartItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_FnbCartId_FnbProductId",
                table: "CartItems",
                newName: "IX_CartItems_CartId_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Transactions",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxAmount",
                table: "Transactions",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "Transactions",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "CinemaId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceModifier",
                table: "SeatSections",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "MovieTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "MovieTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserId",
                table: "MovieTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MovieTickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TrailerPath",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Movies",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "MovieImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Halls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserId",
                table: "Halls",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "FnbProducts",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "FnbOrderItems",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "FnbOrderItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SessionKey",
                table: "FnbCarts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MovieTitleSnapshot",
                table: "FavoriteMovies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cinemas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserId",
                table: "Cinemas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieTypes_CreatedByUserId",
                table: "MovieTypes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTypes_UpdatedByUserId",
                table: "MovieTypes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_UpdatedByUserId",
                table: "Halls",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_UpdatedByUserId",
                table: "Cinemas",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_FnbCarts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "FnbCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_FnbProducts_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "FnbProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Merchants_MerchantId",
                table: "Cinemas",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Users_CreatedByUserId",
                table: "Cinemas",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Users_UpdatedByUserId",
                table: "Cinemas",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMovies_Movies_MovieId",
                table: "FavoriteMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Users_CreatedByUserId",
                table: "Halls",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Users_UpdatedByUserId",
                table: "Halls",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieTypes_MovieTypeId",
                table: "Movies",
                column: "MovieTypeId",
                principalTable: "MovieTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_CreatedByUserId",
                table: "Movies",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTickets_Users_StaffId",
                table: "MovieTickets",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTypes_Users_CreatedByUserId",
                table: "MovieTypes",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTypes_Users_UpdatedByUserId",
                table: "MovieTypes",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
