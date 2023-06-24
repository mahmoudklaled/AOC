using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Migrations
{
    /// <inheritdoc />
    public partial class AddReactsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reacts_ProfileAccounts_ProfileAccountId",
                table: "Reacts");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileAccountId",
                table: "Reacts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Reacts_ProfileAccounts_ProfileAccountId",
                table: "Reacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reacts_ProfileAccounts_ProfileAccountId",
                table: "Reacts");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileAccountId",
                table: "Reacts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reacts_ProfileAccounts_ProfileAccountId",
                table: "Reacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
