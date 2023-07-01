using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class addEmailConfigration : Migration
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

        }
    }
}
