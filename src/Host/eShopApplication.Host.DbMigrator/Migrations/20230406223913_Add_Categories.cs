using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopApplication.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Add_Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Advert",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Advert",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Advert",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Advert",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Advert",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advert_CategoryId",
                table: "Advert",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advert_Category_CategoryId",
                table: "Advert",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advert_Category_CategoryId",
                table: "Advert");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Advert_CategoryId",
                table: "Advert");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Advert");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Advert");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Advert");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Advert");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Advert",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
