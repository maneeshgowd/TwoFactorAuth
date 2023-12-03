using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoFactorAuth.Migrations
{
    /// <inheritdoc />
    public partial class Addedotpmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersOtp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Otp = table.Column<int>(type: "int", nullable: false),
                    RegisteringUserEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersOtp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersOtp_Users_RegisteringUserEmail",
                        column: x => x.RegisteringUserEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersOtp_RegisteringUserEmail",
                table: "UsersOtp",
                column: "RegisteringUserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersOtp");
        }
    }
}
