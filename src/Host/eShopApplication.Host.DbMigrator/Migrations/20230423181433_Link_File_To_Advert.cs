using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopApplication.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Link_File_To_Advert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileIds",
                table: "Advert",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileIds",
                table: "Advert");
        }
    }
}
