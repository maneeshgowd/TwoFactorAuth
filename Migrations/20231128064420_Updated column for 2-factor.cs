using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoFactorAuth.Migrations
{
    /// <inheritdoc />
    public partial class Updatedcolumnfor2factor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTwoFactorAuthEnabled",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTwoFactorAuthEnabled",
                table: "Users");
        }
    }
}
