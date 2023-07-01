using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class addConfigrationFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "PostCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionCommentReacts");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "PostCommentReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionCommentReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "PostCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionCommentReacts");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "PostCommentReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionCommentReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
