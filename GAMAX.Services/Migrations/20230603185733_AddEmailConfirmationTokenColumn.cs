using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GAMAX.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirmationTokenColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                            name: "EmailConfirmationToken",
                            table: "AspNetUsers",
                            nullable: true);
            migrationBuilder.AddColumn<string>(
                            name: "PasswordResetToken",
                            table: "AspNetUsers",
                            nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                            name: "EmailConfirmationToken",
                            table: "AspNetUsers");
            migrationBuilder.DropColumn(
                            name: "PasswordResetToken",
                            table: "AspNetUsers");

        }
    }
}
