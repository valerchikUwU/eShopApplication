using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopApplication.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Link_Account_And_Advert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Advert",
                type: "decimal(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Advert",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Account",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Advert_Account_Id",
                table: "Advert",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advert_Account_Id",
                table: "Advert");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Advert");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Advert",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldPrecision: 15,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);
        }
    }
}
