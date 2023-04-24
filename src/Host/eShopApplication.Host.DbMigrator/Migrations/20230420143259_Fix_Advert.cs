using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopApplication.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Advert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advert_Account_Id",
                table: "Advert");

            migrationBuilder.CreateIndex(
                name: "IX_Advert_AccountId",
                table: "Advert",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advert_Account_AccountId",
                table: "Advert",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advert_Account_AccountId",
                table: "Advert");

            migrationBuilder.DropIndex(
                name: "IX_Advert_AccountId",
                table: "Advert");

            migrationBuilder.AddForeignKey(
                name: "FK_Advert_Account_Id",
                table: "Advert",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
