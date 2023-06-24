using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ProfileAccounts_ProfileAccountId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_ProfileAccounts_ProfileAccountId",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Vedios_Posts_PostId",
                table: "Vedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Vedios_QuestionPosts_QuestionPostId",
                table: "Vedios");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ProfileAccounts_ProfileAccountId",
                table: "Posts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_ProfileAccounts_ProfileAccountId",
                table: "QuestionPosts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vedios_Posts_PostId",
                table: "Vedios",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vedios_QuestionPosts_QuestionPostId",
                table: "Vedios",
                column: "QuestionPostId",
                principalTable: "QuestionPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ProfileAccounts_ProfileAccountId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_ProfileAccounts_ProfileAccountId",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Vedios_Posts_PostId",
                table: "Vedios");

            migrationBuilder.DropForeignKey(
                name: "FK_Vedios_QuestionPosts_QuestionPostId",
                table: "Vedios");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ProfileAccounts_ProfileAccountId",
                table: "Posts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_ProfileAccounts_ProfileAccountId",
                table: "QuestionPosts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vedios_Posts_PostId",
                table: "Vedios",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vedios_QuestionPosts_QuestionPostId",
                table: "Vedios",
                column: "QuestionPostId",
                principalTable: "QuestionPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
