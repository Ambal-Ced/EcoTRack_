using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoTRack_.Migrations
{
    /// <inheritdoc />
    public partial class lde12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNmber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNmber",
                table: "AspNetUsers");
        }
    }
}
