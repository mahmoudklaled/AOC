using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class editsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotos_UserAccounts_ProfileId",
                table: "CoverPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReacts_UserAccounts_ProfileAccountId",
                table: "PostCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_UserAccounts_ProfileAccountId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReacts_UserAccounts_ProfileAccountId",
                table: "PostReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UserAccounts_ProfileAccountId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_UserAccounts_ProfileId",
                table: "ProfilePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentReacts_UserAccounts_ProfileAccountId",
                table: "QuestionCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionComments_UserAccounts_ProfileAccountId",
                table: "QuestionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_UserAccounts_ProfileAccountId",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionReacts_UserAccounts_ProfileAccountId",
                table: "QuestionReacts");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "QuestionReacts",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionReacts_ProfileAccountId",
                table: "QuestionReacts",
                newName: "IX_QuestionReacts_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "QuestionPosts",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionPosts_ProfileAccountId",
                table: "QuestionPosts",
                newName: "IX_QuestionPosts_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "QuestionComments",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionComments_ProfileAccountId",
                table: "QuestionComments",
                newName: "IX_QuestionComments_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "QuestionCommentReacts",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCommentReacts_ProfileAccountId",
                table: "QuestionCommentReacts",
                newName: "IX_QuestionCommentReacts_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "ProfilePhotos",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfilePhotos_ProfileId",
                table: "ProfilePhotos",
                newName: "IX_ProfilePhotos_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "Posts",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_ProfileAccountId",
                table: "Posts",
                newName: "IX_Posts_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "PostReacts",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReacts_ProfileAccountId",
                table: "PostReacts",
                newName: "IX_PostReacts_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "PostComments",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_ProfileAccountId",
                table: "PostComments",
                newName: "IX_PostComments_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileAccountId",
                table: "PostCommentReacts",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCommentReacts_ProfileAccountId",
                table: "PostCommentReacts",
                newName: "IX_PostCommentReacts_UserAccountsId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "CoverPhotos",
                newName: "UserAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_CoverPhotos_ProfileId",
                table: "CoverPhotos",
                newName: "IX_CoverPhotos_UserAccountsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotos_UserAccounts_UserAccountsId",
                table: "CoverPhotos",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReacts_UserAccounts_UserAccountsId",
                table: "PostCommentReacts",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_UserAccounts_UserAccountsId",
                table: "PostComments",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReacts_UserAccounts_UserAccountsId",
                table: "PostReacts",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UserAccounts_UserAccountsId",
                table: "Posts",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_UserAccounts_UserAccountsId",
                table: "ProfilePhotos",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentReacts_UserAccounts_UserAccountsId",
                table: "QuestionCommentReacts",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionComments_UserAccounts_UserAccountsId",
                table: "QuestionComments",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_UserAccounts_UserAccountsId",
                table: "QuestionPosts",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionReacts_UserAccounts_UserAccountsId",
                table: "QuestionReacts",
                column: "UserAccountsId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotos_UserAccounts_UserAccountsId",
                table: "CoverPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReacts_UserAccounts_UserAccountsId",
                table: "PostCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_UserAccounts_UserAccountsId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReacts_UserAccounts_UserAccountsId",
                table: "PostReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UserAccounts_UserAccountsId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_UserAccounts_UserAccountsId",
                table: "ProfilePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentReacts_UserAccounts_UserAccountsId",
                table: "QuestionCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionComments_UserAccounts_UserAccountsId",
                table: "QuestionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_UserAccounts_UserAccountsId",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionReacts_UserAccounts_UserAccountsId",
                table: "QuestionReacts");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "QuestionReacts",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionReacts_UserAccountsId",
                table: "QuestionReacts",
                newName: "IX_QuestionReacts_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "QuestionPosts",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionPosts_UserAccountsId",
                table: "QuestionPosts",
                newName: "IX_QuestionPosts_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "QuestionComments",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionComments_UserAccountsId",
                table: "QuestionComments",
                newName: "IX_QuestionComments_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "QuestionCommentReacts",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionCommentReacts_UserAccountsId",
                table: "QuestionCommentReacts",
                newName: "IX_QuestionCommentReacts_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "ProfilePhotos",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfilePhotos_UserAccountsId",
                table: "ProfilePhotos",
                newName: "IX_ProfilePhotos_ProfileId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "Posts",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserAccountsId",
                table: "Posts",
                newName: "IX_Posts_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "PostReacts",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReacts_UserAccountsId",
                table: "PostReacts",
                newName: "IX_PostReacts_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "PostComments",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_UserAccountsId",
                table: "PostComments",
                newName: "IX_PostComments_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "PostCommentReacts",
                newName: "ProfileAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCommentReacts_UserAccountsId",
                table: "PostCommentReacts",
                newName: "IX_PostCommentReacts_ProfileAccountId");

            migrationBuilder.RenameColumn(
                name: "UserAccountsId",
                table: "CoverPhotos",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CoverPhotos_UserAccountsId",
                table: "CoverPhotos",
                newName: "IX_CoverPhotos_ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotos_UserAccounts_ProfileId",
                table: "CoverPhotos",
                column: "ProfileId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReacts_UserAccounts_ProfileAccountId",
                table: "PostCommentReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_UserAccounts_ProfileAccountId",
                table: "PostComments",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReacts_UserAccounts_ProfileAccountId",
                table: "PostReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UserAccounts_ProfileAccountId",
                table: "Posts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_UserAccounts_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentReacts_UserAccounts_ProfileAccountId",
                table: "QuestionCommentReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionComments_UserAccounts_ProfileAccountId",
                table: "QuestionComments",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_UserAccounts_ProfileAccountId",
                table: "QuestionPosts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionReacts_UserAccounts_ProfileAccountId",
                table: "QuestionReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
