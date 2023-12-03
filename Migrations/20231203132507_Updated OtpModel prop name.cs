using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoFactorAuth.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOtpModelpropname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersOtp_Users_RegisteringUserEmail",
                table: "UsersOtp");

            migrationBuilder.RenameColumn(
                name: "RegisteringUserEmail",
                table: "UsersOtp",
                newName: "RegisterModelEmail");

            migrationBuilder.RenameIndex(
                name: "IX_UsersOtp_RegisteringUserEmail",
                table: "UsersOtp",
                newName: "IX_UsersOtp_RegisterModelEmail");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "UsersOtp",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersOtp_Users_RegisterModelEmail",
                table: "UsersOtp",
                column: "RegisterModelEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersOtp_Users_RegisterModelEmail",
                table: "UsersOtp");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "UsersOtp");

            migrationBuilder.RenameColumn(
                name: "RegisterModelEmail",
                table: "UsersOtp",
                newName: "RegisteringUserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_UsersOtp_RegisterModelEmail",
                table: "UsersOtp",
                newName: "IX_UsersOtp_RegisteringUserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersOtp_Users_RegisteringUserEmail",
                table: "UsersOtp",
                column: "RegisteringUserEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
